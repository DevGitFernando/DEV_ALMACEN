namespace Almacen.Reportes
{
    partial class Frm_RptListadoPorCosto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_RptListadoPorCosto));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblDescripcionSal = new SC_ControlsCS.scLabelExt();
            this.lstResultado = new System.Windows.Forms.ListView();
            this.chkConUbicaciones = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.nmRangoFinal = new System.Windows.Forms.NumericUpDown();
            this.nmRangoInicial = new System.Windows.Forms.NumericUpDown();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmRangoFinal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmRangoInicial)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(984, 25);
            this.toolStripBarraMenu.TabIndex = 9;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblDescripcionSal);
            this.groupBox2.Controls.Add(this.lstResultado);
            this.groupBox2.Controls.Add(this.chkConUbicaciones);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Location = new System.Drawing.Point(9, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(966, 478);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Existencias";
            // 
            // lblDescripcionSal
            // 
            this.lblDescripcionSal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionSal.Location = new System.Drawing.Point(160, 347);
            this.lblDescripcionSal.MostrarToolTip = false;
            this.lblDescripcionSal.Name = "lblDescripcionSal";
            this.lblDescripcionSal.Size = new System.Drawing.Size(616, 23);
            this.lblDescripcionSal.TabIndex = 44;
            this.lblDescripcionSal.Text = "scLabelExt1";
            this.lblDescripcionSal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDescripcionSal.Visible = false;
            // 
            // lstResultado
            // 
            this.lstResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResultado.Location = new System.Drawing.Point(10, 16);
            this.lstResultado.Name = "lstResultado";
            this.lstResultado.Size = new System.Drawing.Size(946, 456);
            this.lstResultado.TabIndex = 8;
            this.lstResultado.UseCompatibleStateImageBehavior = false;
            this.lstResultado.View = System.Windows.Forms.View.Details;
            // 
            // chkConUbicaciones
            // 
            this.chkConUbicaciones.Enabled = false;
            this.chkConUbicaciones.Location = new System.Drawing.Point(241, 186);
            this.chkConUbicaciones.Name = "chkConUbicaciones";
            this.chkConUbicaciones.Size = new System.Drawing.Size(95, 24);
            this.chkConUbicaciones.TabIndex = 6;
            this.chkConUbicaciones.Text = "Con ubicaciones";
            this.chkConUbicaciones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConUbicaciones.UseVisualStyleBackColor = true;
            this.chkConUbicaciones.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(623, 422);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Existencia Total :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(743, 422);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(124, 23);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "label3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.scLabelExt2);
            this.groupBox1.Controls.Add(this.scLabelExt1);
            this.groupBox1.Controls.Add(this.nmRangoFinal);
            this.groupBox1.Controls.Add(this.nmRangoInicial);
            this.groupBox1.Location = new System.Drawing.Point(9, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(966, 44);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parámetros";
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.scLabelExt2.Location = new System.Drawing.Point(497, 19);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(18, 18);
            this.scLabelExt2.TabIndex = 9;
            this.scLabelExt2.Text = "y";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.scLabelExt1.Location = new System.Drawing.Point(338, 19);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(41, 18);
            this.scLabelExt1.TabIndex = 8;
            this.scLabelExt1.Text = "Entre ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmRangoFinal
            // 
            this.nmRangoFinal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nmRangoFinal.DecimalPlaces = 2;
            this.nmRangoFinal.Location = new System.Drawing.Point(523, 18);
            this.nmRangoFinal.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nmRangoFinal.Name = "nmRangoFinal";
            this.nmRangoFinal.Size = new System.Drawing.Size(106, 20);
            this.nmRangoFinal.TabIndex = 7;
            this.nmRangoFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nmRangoInicial
            // 
            this.nmRangoInicial.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nmRangoInicial.DecimalPlaces = 2;
            this.nmRangoInicial.Location = new System.Drawing.Point(381, 18);
            this.nmRangoInicial.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nmRangoInicial.Name = "nmRangoInicial";
            this.nmRangoInicial.Size = new System.Drawing.Size(106, 20);
            this.nmRangoInicial.TabIndex = 6;
            this.nmRangoInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Frm_RptListadoPorCosto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "Frm_RptListadoPorCosto";
            this.Text = "Reporte de Ubicaciones por Costo";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.Frm_RptListadoPorCosto_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmRangoFinal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmRangoInicial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox groupBox2;
        private SC_ControlsCS.scLabelExt lblDescripcionSal;
        private System.Windows.Forms.ListView lstResultado;
        private System.Windows.Forms.CheckBox chkConUbicaciones;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private System.Windows.Forms.NumericUpDown nmRangoFinal;
        private System.Windows.Forms.NumericUpDown nmRangoInicial;
    }
}