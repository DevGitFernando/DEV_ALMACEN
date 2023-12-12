namespace MA_Facturacion.CuentasPorPagar.Cheques
{
    partial class FrmListadoDeCheques
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoDeCheques));
            this.FrameFacturas = new System.Windows.Forms.GroupBox();
            this.lstCheques = new SC_ControlsCS.scListView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.chkTodasFechas = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameTipoFacturas = new System.Windows.Forms.GroupBox();
            this.rdoCancelado = new System.Windows.Forms.RadioButton();
            this.rdoActivos = new System.Windows.Forms.RadioButton();
            this.rdoTodos = new System.Windows.Forms.RadioButton();
            this.FrameFacturas.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameTipoFacturas.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameFacturas
            // 
            this.FrameFacturas.Controls.Add(this.lstCheques);
            this.FrameFacturas.Location = new System.Drawing.Point(12, 103);
            this.FrameFacturas.Name = "FrameFacturas";
            this.FrameFacturas.Size = new System.Drawing.Size(844, 307);
            this.FrameFacturas.TabIndex = 3;
            this.FrameFacturas.TabStop = false;
            this.FrameFacturas.Text = "Listado de Facturas";
            // 
            // lstCheques
            // 
            this.lstCheques.Location = new System.Drawing.Point(12, 19);
            this.lstCheques.LockColumnSize = false;
            this.lstCheques.Name = "lstCheques";
            this.lstCheques.Size = new System.Drawing.Size(820, 283);
            this.lstCheques.TabIndex = 6;
            this.lstCheques.UseCompatibleStateImageBehavior = false;
            this.lstCheques.View = System.Windows.Forms.View.Details;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnExportar,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(866, 25);
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
            // btnExportar
            // 
            this.btnExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(23, 22);
            this.btnExportar.Text = "&Imprimir";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFin);
            this.FrameFechas.Controls.Add(this.chkTodasFechas);
            this.FrameFechas.Controls.Add(this.label1);
            this.FrameFechas.Controls.Add(this.dtpFechaInicio);
            this.FrameFechas.Controls.Add(this.label9);
            this.FrameFechas.Location = new System.Drawing.Point(12, 28);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(357, 69);
            this.FrameFechas.TabIndex = 1;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de Fechas";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(103, 42);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(94, 20);
            this.dtpFechaFin.TabIndex = 2;
            // 
            // chkTodasFechas
            // 
            this.chkTodasFechas.AutoSize = true;
            this.chkTodasFechas.Location = new System.Drawing.Point(228, 31);
            this.chkTodasFechas.Name = "chkTodasFechas";
            this.chkTodasFechas.Size = new System.Drawing.Size(110, 17);
            this.chkTodasFechas.TabIndex = 0;
            this.chkTodasFechas.Text = "Todas las Fechas";
            this.chkTodasFechas.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Fecha Fin :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(103, 16);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(94, 20);
            this.dtpFechaInicio.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(19, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Fecha Inicio :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(692, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 69);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales";
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(66, 28);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(86, 20);
            this.lblTotal.TabIndex = 56;
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Total :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameTipoFacturas
            // 
            this.FrameTipoFacturas.Controls.Add(this.rdoCancelado);
            this.FrameTipoFacturas.Controls.Add(this.rdoActivos);
            this.FrameTipoFacturas.Controls.Add(this.rdoTodos);
            this.FrameTipoFacturas.Location = new System.Drawing.Point(375, 28);
            this.FrameTipoFacturas.Name = "FrameTipoFacturas";
            this.FrameTipoFacturas.Size = new System.Drawing.Size(311, 69);
            this.FrameTipoFacturas.TabIndex = 11;
            this.FrameTipoFacturas.TabStop = false;
            this.FrameTipoFacturas.Text = "Status";
            // 
            // rdoCancelado
            // 
            this.rdoCancelado.Location = new System.Drawing.Point(190, 29);
            this.rdoCancelado.Name = "rdoCancelado";
            this.rdoCancelado.Size = new System.Drawing.Size(85, 17);
            this.rdoCancelado.TabIndex = 2;
            this.rdoCancelado.Text = "Cancelados";
            this.rdoCancelado.UseVisualStyleBackColor = true;
            // 
            // rdoActivos
            // 
            this.rdoActivos.Location = new System.Drawing.Point(105, 28);
            this.rdoActivos.Name = "rdoActivos";
            this.rdoActivos.Size = new System.Drawing.Size(79, 17);
            this.rdoActivos.TabIndex = 1;
            this.rdoActivos.Text = "Activos";
            this.rdoActivos.UseVisualStyleBackColor = true;
            // 
            // rdoTodos
            // 
            this.rdoTodos.Checked = true;
            this.rdoTodos.Location = new System.Drawing.Point(36, 28);
            this.rdoTodos.Name = "rdoTodos";
            this.rdoTodos.Size = new System.Drawing.Size(63, 17);
            this.rdoTodos.TabIndex = 0;
            this.rdoTodos.TabStop = true;
            this.rdoTodos.Text = "Todos";
            this.rdoTodos.UseVisualStyleBackColor = true;
            // 
            // FrmListadoDeCheques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 416);
            this.Controls.Add(this.FrameTipoFacturas);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameFacturas);
            this.Name = "FrmListadoDeCheques";
            this.Text = "Listado de Cheques Emitidos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoDeCheques_Load);
            this.FrameFacturas.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.FrameFechas.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.FrameTipoFacturas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameFacturas;
        private SC_ControlsCS.scListView lstCheques;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.CheckBox chkTodasFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FrameTipoFacturas;
        private System.Windows.Forms.RadioButton rdoCancelado;
        private System.Windows.Forms.RadioButton rdoActivos;
        private System.Windows.Forms.RadioButton rdoTodos;
        private System.Windows.Forms.Label lblTotal;

    }
}