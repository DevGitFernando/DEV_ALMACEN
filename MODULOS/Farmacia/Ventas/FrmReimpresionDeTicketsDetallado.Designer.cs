namespace Farmacia.Ventas
{
    partial class FrmReimpresionDeTicketsDetallado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReimpresionDeTicketsDetallado));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.txtFolioFinal = new SC_ControlsCS.scTextBoxExt();
            this.lblFolioFinal = new System.Windows.Forms.Label();
            this.chkMostrarPrecios = new System.Windows.Forms.CheckBox();
            this.chkMostrarImpresionEnPantalla = new System.Windows.Forms.CheckBox();
            this.txtAux = new SC_ControlsCS.scTextBoxExt();
            this.rdoVentaCredito = new System.Windows.Forms.RadioButton();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.menuPrecios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAplicarPrecios = new System.Windows.Forms.ToolStripMenuItem();
            this.rdoVentaContado = new System.Windows.Forms.RadioButton();
            this.lblFolio = new System.Windows.Forms.Label();
            this.tmImpresion = new System.Windows.Forms.Timer(this.components);
            this.lblOpciones = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            this.menuPrecios.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnImprimir,
            this.toolStripSeparator1,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(445, 25);
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
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.txtFolioFinal);
            this.FrameDatos.Controls.Add(this.lblFolioFinal);
            this.FrameDatos.Controls.Add(this.chkMostrarPrecios);
            this.FrameDatos.Controls.Add(this.chkMostrarImpresionEnPantalla);
            this.FrameDatos.Controls.Add(this.txtAux);
            this.FrameDatos.Controls.Add(this.rdoVentaCredito);
            this.FrameDatos.Controls.Add(this.txtFolio);
            this.FrameDatos.Controls.Add(this.rdoVentaContado);
            this.FrameDatos.Controls.Add(this.lblFolio);
            this.FrameDatos.Location = new System.Drawing.Point(13, 32);
            this.FrameDatos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatos.Size = new System.Drawing.Size(420, 116);
            this.FrameDatos.TabIndex = 1;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Datos del ticket";
            // 
            // txtFolioFinal
            // 
            this.txtFolioFinal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioFinal.Decimales = 2;
            this.txtFolioFinal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioFinal.ForeColor = System.Drawing.Color.Black;
            this.txtFolioFinal.Location = new System.Drawing.Point(269, 130);
            this.txtFolioFinal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFolioFinal.MaxLength = 8;
            this.txtFolioFinal.Name = "txtFolioFinal";
            this.txtFolioFinal.PermitirApostrofo = false;
            this.txtFolioFinal.PermitirNegativos = false;
            this.txtFolioFinal.Size = new System.Drawing.Size(132, 22);
            this.txtFolioFinal.TabIndex = 1;
            this.txtFolioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolioFinal.Visible = false;
            this.txtFolioFinal.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolioFinal_Validating);
            // 
            // lblFolioFinal
            // 
            this.lblFolioFinal.Location = new System.Drawing.Point(165, 132);
            this.lblFolioFinal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFolioFinal.Name = "lblFolioFinal";
            this.lblFolioFinal.Size = new System.Drawing.Size(100, 17);
            this.lblFolioFinal.TabIndex = 53;
            this.lblFolioFinal.Text = "Folio final :";
            this.lblFolioFinal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFolioFinal.Visible = false;
            // 
            // chkMostrarPrecios
            // 
            this.chkMostrarPrecios.Location = new System.Drawing.Point(112, 87);
            this.chkMostrarPrecios.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMostrarPrecios.Name = "chkMostrarPrecios";
            this.chkMostrarPrecios.Size = new System.Drawing.Size(231, 21);
            this.chkMostrarPrecios.TabIndex = 51;
            this.chkMostrarPrecios.Text = "Mostrar precios";
            this.chkMostrarPrecios.UseVisualStyleBackColor = true;
            // 
            // chkMostrarImpresionEnPantalla
            // 
            this.chkMostrarImpresionEnPantalla.Location = new System.Drawing.Point(112, 59);
            this.chkMostrarImpresionEnPantalla.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMostrarImpresionEnPantalla.Name = "chkMostrarImpresionEnPantalla";
            this.chkMostrarImpresionEnPantalla.Size = new System.Drawing.Size(231, 21);
            this.chkMostrarImpresionEnPantalla.TabIndex = 49;
            this.chkMostrarImpresionEnPantalla.Text = "Mostrar vista previa impresión ";
            this.chkMostrarImpresionEnPantalla.UseVisualStyleBackColor = true;
            // 
            // txtAux
            // 
            this.txtAux.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtAux.Decimales = 2;
            this.txtAux.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtAux.ForeColor = System.Drawing.Color.Black;
            this.txtAux.Location = new System.Drawing.Point(112, 208);
            this.txtAux.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAux.MaxLength = 8;
            this.txtAux.Name = "txtAux";
            this.txtAux.PermitirApostrofo = false;
            this.txtAux.PermitirNegativos = false;
            this.txtAux.Size = new System.Drawing.Size(132, 22);
            this.txtAux.TabIndex = 4;
            this.txtAux.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rdoVentaCredito
            // 
            this.rdoVentaCredito.Location = new System.Drawing.Point(269, 36);
            this.rdoVentaCredito.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoVentaCredito.Name = "rdoVentaCredito";
            this.rdoVentaCredito.Size = new System.Drawing.Size(148, 21);
            this.rdoVentaCredito.TabIndex = 3;
            this.rdoVentaCredito.Text = "Venta de Crédito";
            this.rdoVentaCredito.UseVisualStyleBackColor = true;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.ContextMenuStrip = this.menuPrecios;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(112, 27);
            this.txtFolio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(132, 22);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // menuPrecios
            // 
            this.menuPrecios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAplicarPrecios});
            this.menuPrecios.Name = "menuPrecios";
            this.menuPrecios.Size = new System.Drawing.Size(197, 28);
            // 
            // btnAplicarPrecios
            // 
            this.btnAplicarPrecios.Name = "btnAplicarPrecios";
            this.btnAplicarPrecios.Size = new System.Drawing.Size(196, 24);
            this.btnAplicarPrecios.Text = "Actualizar precios";
            this.btnAplicarPrecios.Click += new System.EventHandler(this.btnAplicarPrecios_Click);
            // 
            // rdoVentaContado
            // 
            this.rdoVentaContado.Location = new System.Drawing.Point(269, 14);
            this.rdoVentaContado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoVentaContado.Name = "rdoVentaContado";
            this.rdoVentaContado.Size = new System.Drawing.Size(148, 21);
            this.rdoVentaContado.TabIndex = 2;
            this.rdoVentaContado.Text = "Venta de Contado";
            this.rdoVentaContado.UseVisualStyleBackColor = true;
            // 
            // lblFolio
            // 
            this.lblFolio.Location = new System.Drawing.Point(12, 28);
            this.lblFolio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(96, 17);
            this.lblFolio.TabIndex = 3;
            this.lblFolio.Text = "Folio inicial :";
            this.lblFolio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmImpresion
            // 
            this.tmImpresion.Interval = 500;
            this.tmImpresion.Tick += new System.EventHandler(this.tmImpresion_Tick);
            // 
            // lblOpciones
            // 
            this.lblOpciones.BackColor = System.Drawing.Color.Black;
            this.lblOpciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblOpciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpciones.ForeColor = System.Drawing.SystemColors.Control;
            this.lblOpciones.Location = new System.Drawing.Point(0, 152);
            this.lblOpciones.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOpciones.Name = "lblOpciones";
            this.lblOpciones.Size = new System.Drawing.Size(445, 30);
            this.lblOpciones.TabIndex = 11;
            this.lblOpciones.Text = "<Clic derecho> Actualizar precios de venta";
            this.lblOpciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmReimpresionDeTicketsDetallado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 182);
            this.ContextMenuStrip = this.menuPrecios;
            this.Controls.Add(this.lblOpciones);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmReimpresionDeTicketsDetallado";
            this.Text = "Reimpresión de Tickets : Detallado";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Activated += new System.EventHandler(this.FrmReimpresionDeTicketsDetallado_Activated);
            this.Load += new System.EventHandler(this.FrmReimpresionDeTicketsDetallado_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatos.ResumeLayout(false);
            this.FrameDatos.PerformLayout();
            this.menuPrecios.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameDatos;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.RadioButton rdoVentaContado;
        private System.Windows.Forms.RadioButton rdoVentaCredito;
        private SC_ControlsCS.scTextBoxExt txtAux;
        private System.Windows.Forms.CheckBox chkMostrarImpresionEnPantalla;
        private System.Windows.Forms.Timer tmImpresion;
        private System.Windows.Forms.CheckBox chkMostrarPrecios;
        private SC_ControlsCS.scTextBoxExt txtFolioFinal;
        private System.Windows.Forms.Label lblFolioFinal;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.Label lblOpciones;
        private System.Windows.Forms.ContextMenuStrip menuPrecios;
        private System.Windows.Forms.ToolStripMenuItem btnAplicarPrecios;
    }
}