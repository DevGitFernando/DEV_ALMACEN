namespace DllFarmaciaSoft.OrdenesDeCompra
{
    partial class FrmVerificarIngresosOrdenDeCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVerificarIngresosOrdenDeCompra));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimirOrdenDeCompra = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstDetalles = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstClaves = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblIdProveedor = new System.Windows.Forms.Label();
            this.lblRecibida = new System.Windows.Forms.Label();
            this.txtOrden = new SC_ControlsCS.scTextBoxExt();
            this.label13 = new System.Windows.Forms.Label();
            this.lblNombreProveedor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator3,
            this.btnImprimir,
            this.toolStripSeparator5,
            this.btnImprimirOrdenDeCompra,
            this.toolStripSeparator4});
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
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
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
            this.btnEjecutar.Click += new System.EventHandler(this.btnRecepcionesOC_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator5.Visible = false;
            // 
            // btnImprimirOrdenDeCompra
            // 
            this.btnImprimirOrdenDeCompra.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimirOrdenDeCompra.Enabled = false;
            this.btnImprimirOrdenDeCompra.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimirOrdenDeCompra.Image")));
            this.btnImprimirOrdenDeCompra.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimirOrdenDeCompra.Name = "btnImprimirOrdenDeCompra";
            this.btnImprimirOrdenDeCompra.Size = new System.Drawing.Size(23, 22);
            this.btnImprimirOrdenDeCompra.Text = "Imprimir Orden de Compra";
            this.btnImprimirOrdenDeCompra.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lstDetalles);
            this.groupBox2.Location = new System.Drawing.Point(10, 359);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(864, 248);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles por Folios";
            // 
            // lstDetalles
            // 
            this.lstDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDetalles.HideSelection = false;
            this.lstDetalles.Location = new System.Drawing.Point(10, 16);
            this.lstDetalles.Name = "lstDetalles";
            this.lstDetalles.Size = new System.Drawing.Size(844, 222);
            this.lstDetalles.TabIndex = 0;
            this.lstDetalles.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lstClaves);
            this.groupBox1.Location = new System.Drawing.Point(10, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(864, 248);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Concentrado por Claves";
            // 
            // lstClaves
            // 
            this.lstClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstClaves.HideSelection = false;
            this.lstClaves.Location = new System.Drawing.Point(10, 16);
            this.lstClaves.Name = "lstClaves";
            this.lstClaves.Size = new System.Drawing.Size(844, 222);
            this.lstClaves.TabIndex = 0;
            this.lstClaves.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblIdProveedor);
            this.groupBox3.Controls.Add(this.lblRecibida);
            this.groupBox3.Controls.Add(this.txtOrden);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.lblNombreProveedor);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(10, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(864, 78);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Orden de Compra";
            // 
            // lblIdProveedor
            // 
            this.lblIdProveedor.BackColor = System.Drawing.Color.Transparent;
            this.lblIdProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdProveedor.Location = new System.Drawing.Point(100, 42);
            this.lblIdProveedor.Name = "lblIdProveedor";
            this.lblIdProveedor.Size = new System.Drawing.Size(98, 20);
            this.lblIdProveedor.TabIndex = 2;
            this.lblIdProveedor.Text = "RECIBIDA";
            this.lblIdProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRecibida
            // 
            this.lblRecibida.BackColor = System.Drawing.Color.Transparent;
            this.lblRecibida.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRecibida.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecibida.Location = new System.Drawing.Point(206, 18);
            this.lblRecibida.Name = "lblRecibida";
            this.lblRecibida.Size = new System.Drawing.Size(98, 20);
            this.lblRecibida.TabIndex = 1;
            this.lblRecibida.Text = "RECIBIDA";
            this.lblRecibida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRecibida.Visible = false;
            // 
            // txtOrden
            // 
            this.txtOrden.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtOrden.Decimales = 2;
            this.txtOrden.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtOrden.ForeColor = System.Drawing.Color.Black;
            this.txtOrden.Location = new System.Drawing.Point(100, 18);
            this.txtOrden.MaxLength = 8;
            this.txtOrden.Name = "txtOrden";
            this.txtOrden.PermitirApostrofo = false;
            this.txtOrden.PermitirNegativos = false;
            this.txtOrden.Size = new System.Drawing.Size(100, 20);
            this.txtOrden.TabIndex = 0;
            this.txtOrden.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOrden.Validating += new System.ComponentModel.CancelEventHandler(this.txtOrden_Validating);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(12, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 12);
            this.label13.TabIndex = 31;
            this.label13.Text = "Orden Compra :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNombreProveedor
            // 
            this.lblNombreProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombreProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombreProveedor.Location = new System.Drawing.Point(206, 42);
            this.lblNombreProveedor.Name = "lblNombreProveedor";
            this.lblNombreProveedor.Size = new System.Drawing.Size(648, 21);
            this.lblNombreProveedor.TabIndex = 3;
            this.lblNombreProveedor.Text = "Proveedor :";
            this.lblNombreProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(30, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "Proveedor :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmVerificarIngresosOrdenDeCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 611);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmVerificarIngresosOrdenDeCompra";
            this.Text = "Verificar Recepción de Orden de Compra";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmVerificarIngresosOrdenDeCompra_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnImprimirOrdenDeCompra;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstDetalles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstClaves;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblRecibida;
        private SC_ControlsCS.scTextBoxExt txtOrden;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblNombreProveedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIdProveedor;
    }
}