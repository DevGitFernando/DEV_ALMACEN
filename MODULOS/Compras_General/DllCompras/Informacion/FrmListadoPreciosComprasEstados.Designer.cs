namespace DllCompras.Informacion
{
    partial class FrmListadoPreciosComprasEstados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoPreciosComprasEstados));
            this.tsBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.lstClaves = new System.Windows.Forms.ListView();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.cboEdo = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.tsBarraMenu.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsBarraMenu
            // 
            this.tsBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnImprimir,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.tsBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.tsBarraMenu.Name = "tsBarraMenu";
            this.tsBarraMenu.Size = new System.Drawing.Size(1084, 25);
            this.tsBarraMenu.TabIndex = 0;
            this.tsBarraMenu.Text = "toolStrip1";
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
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
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
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDetalles.Controls.Add(this.lstClaves);
            this.FrameDetalles.Location = new System.Drawing.Point(10, 80);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(1062, 523);
            this.FrameDetalles.TabIndex = 2;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Listado Precios de Productos Compras--Estados";
            // 
            // lstClaves
            // 
            this.lstClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstClaves.Location = new System.Drawing.Point(7, 18);
            this.lstClaves.Name = "lstClaves";
            this.lstClaves.Size = new System.Drawing.Size(1048, 494);
            this.lstClaves.TabIndex = 0;
            this.lstClaves.UseCompatibleStateImageBehavior = false;
            // 
            // FrameDatos
            // 
            this.FrameDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDatos.Controls.Add(this.label3);
            this.FrameDatos.Controls.Add(this.cboEmpresas);
            this.FrameDatos.Controls.Add(this.cboEdo);
            this.FrameDatos.Controls.Add(this.label4);
            this.FrameDatos.Location = new System.Drawing.Point(10, 28);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(1062, 49);
            this.FrameDatos.TabIndex = 1;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Estados";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.Location = new System.Drawing.Point(86, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Empresa :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(142, 17);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(368, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // cboEdo
            // 
            this.cboEdo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboEdo.BackColorEnabled = System.Drawing.Color.White;
            this.cboEdo.Data = "";
            this.cboEdo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdo.Filtro = " 1 = 1";
            this.cboEdo.FormattingEnabled = true;
            this.cboEdo.ListaItemsBusqueda = 20;
            this.cboEdo.Location = new System.Drawing.Point(608, 17);
            this.cboEdo.MostrarToolTip = false;
            this.cboEdo.Name = "cboEdo";
            this.cboEdo.Size = new System.Drawing.Size(368, 21);
            this.cboEdo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.Location = new System.Drawing.Point(558, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Estado :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmListadoPreciosComprasEstados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 611);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.FrameDetalles);
            this.Controls.Add(this.tsBarraMenu);
            this.Name = "FrmListadoPreciosComprasEstados";
            this.Text = "Listado de Precios de Productos  Compras -- Estados";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoOrdenesDeCompras_Load);
            this.tsBarraMenu.ResumeLayout(false);
            this.tsBarraMenu.PerformLayout();
            this.FrameDetalles.ResumeLayout(false);
            this.FrameDatos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private System.Windows.Forms.ListView lstClaves;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox FrameDatos;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private SC_ControlsCS.scComboBoxExt cboEdo;
        private System.Windows.Forms.Label label4;
    }
}