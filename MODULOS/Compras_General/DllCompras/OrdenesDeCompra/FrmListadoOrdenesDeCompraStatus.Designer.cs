namespace DllCompras.OrdenesDeCompra
{
    partial class FrmListadoOrdenesDeCompraStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoOrdenesDeCompraStatus));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.cboFar = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.cboEdo = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lstPedidos = new System.Windows.Forms.ListView();
            this.IdEmpresa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdEstado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdProveedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProveedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdStatusOrden = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StatusOrden = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFinal = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.txtInicial = new SC_ControlsCS.scTextBoxExt();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSiguiente = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboEmpresas);
            this.groupBox1.Controls.Add(this.cboFar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboEdo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(664, 106);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "Empresa :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(70, 20);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(580, 21);
            this.cboEmpresas.TabIndex = 24;
            this.cboEmpresas.Validating += new System.ComponentModel.CancelEventHandler(this.cboEmpresas_Validating);
            // 
            // cboFar
            // 
            this.cboFar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFar.BackColorEnabled = System.Drawing.Color.White;
            this.cboFar.Data = "";
            this.cboFar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFar.Filtro = " 1 = 1";
            this.cboFar.FormattingEnabled = true;
            this.cboFar.ListaItemsBusqueda = 20;
            this.cboFar.Location = new System.Drawing.Point(70, 74);
            this.cboFar.MostrarToolTip = false;
            this.cboFar.Name = "cboFar";
            this.cboFar.Size = new System.Drawing.Size(580, 21);
            this.cboFar.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Farmacia : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEdo
            // 
            this.cboEdo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEdo.BackColorEnabled = System.Drawing.Color.White;
            this.cboEdo.Data = "";
            this.cboEdo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdo.Filtro = " 1 = 1";
            this.cboEdo.FormattingEnabled = true;
            this.cboEdo.ListaItemsBusqueda = 20;
            this.cboEdo.Location = new System.Drawing.Point(70, 47);
            this.cboEdo.MostrarToolTip = false;
            this.cboEdo.Name = "cboEdo";
            this.cboEdo.Size = new System.Drawing.Size(580, 21);
            this.cboEdo.TabIndex = 0;
            this.cboEdo.Validating += new System.ComponentModel.CancelEventHandler(this.cboEdo_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estado : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstPedidos
            // 
            this.lstPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPedidos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdEmpresa,
            this.IdEstado,
            this.IdFarmacia,
            this.colFolio,
            this.IdProveedor,
            this.colProveedor,
            this.colIdStatusOrden,
            this.StatusOrden});
            this.lstPedidos.Location = new System.Drawing.Point(10, 18);
            this.lstPedidos.Name = "lstPedidos";
            this.lstPedidos.Size = new System.Drawing.Size(842, 368);
            this.lstPedidos.TabIndex = 2;
            this.lstPedidos.UseCompatibleStateImageBehavior = false;
            this.lstPedidos.View = System.Windows.Forms.View.Details;
            // 
            // IdEmpresa
            // 
            this.IdEmpresa.DisplayIndex = 5;
            this.IdEmpresa.Text = "IdEmpresa";
            this.IdEmpresa.Width = 0;
            // 
            // IdEstado
            // 
            this.IdEstado.DisplayIndex = 6;
            this.IdEstado.Text = "IdEstado";
            this.IdEstado.Width = 0;
            // 
            // IdFarmacia
            // 
            this.IdFarmacia.DisplayIndex = 7;
            this.IdFarmacia.Text = "IdFarmacia";
            this.IdFarmacia.Width = 0;
            // 
            // colFolio
            // 
            this.colFolio.DisplayIndex = 0;
            this.colFolio.Text = "Folio Orden";
            this.colFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colFolio.Width = 68;
            // 
            // IdProveedor
            // 
            this.IdProveedor.DisplayIndex = 1;
            this.IdProveedor.Text = "Clave Proveedor";
            this.IdProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IdProveedor.Width = 95;
            // 
            // colProveedor
            // 
            this.colProveedor.DisplayIndex = 2;
            this.colProveedor.Text = "Proveedor";
            this.colProveedor.Width = 301;
            // 
            // colIdStatusOrden
            // 
            this.colIdStatusOrden.DisplayIndex = 3;
            this.colIdStatusOrden.Text = "Clave Status";
            this.colIdStatusOrden.Width = 0;
            // 
            // StatusOrden
            // 
            this.StatusOrden.DisplayIndex = 4;
            this.StatusOrden.Text = "Status De Orden";
            this.StatusOrden.Width = 166;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(884, 25);
            this.toolStripBarraMenu.TabIndex = 17;
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
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lstPedidos);
            this.groupBox2.Location = new System.Drawing.Point(12, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(862, 395);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalle";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtFinal);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtInicial);
            this.groupBox3.Location = new System.Drawing.Point(682, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(192, 106);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Folios";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(14, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 30;
            this.label4.Text = "Folio Final :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFinal
            // 
            this.txtFinal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFinal.Decimales = 2;
            this.txtFinal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFinal.ForeColor = System.Drawing.Color.Black;
            this.txtFinal.Location = new System.Drawing.Point(81, 59);
            this.txtFinal.MaxLength = 8;
            this.txtFinal.Name = "txtFinal";
            this.txtFinal.PermitirApostrofo = false;
            this.txtFinal.PermitirNegativos = false;
            this.txtFinal.Size = new System.Drawing.Size(99, 20);
            this.txtFinal.TabIndex = 29;
            this.txtFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFinal.Validating += new System.ComponentModel.CancelEventHandler(this.txtFinal_Validating);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(14, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 16);
            this.label8.TabIndex = 28;
            this.label8.Text = "Folio Inicio :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInicial
            // 
            this.txtInicial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtInicial.Decimales = 2;
            this.txtInicial.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtInicial.ForeColor = System.Drawing.Color.Black;
            this.txtInicial.Location = new System.Drawing.Point(81, 27);
            this.txtInicial.MaxLength = 8;
            this.txtInicial.Name = "txtInicial";
            this.txtInicial.PermitirApostrofo = false;
            this.txtInicial.PermitirNegativos = false;
            this.txtInicial.Size = new System.Drawing.Size(99, 20);
            this.txtInicial.TabIndex = 27;
            this.txtInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInicial.Validating += new System.ComponentModel.CancelEventHandler(this.txtInicial_Validating);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSiguiente});
            this.menu.Name = "menuCantidades";
            this.menu.Size = new System.Drawing.Size(159, 26);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(158, 22);
            this.btnSiguiente.Text = "Siguiente Status";
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 537);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(884, 24);
            this.lblMensajes.TabIndex = 28;
            this.lblMensajes.Text = " << Clic derecho para opciones >>";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmListadoOrdenesDeCompraStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmListadoOrdenesDeCompraStatus";
            this.Text = "Listado de status ordenes de compras";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoOrdenesDeCompraStatus_Load);
            this.groupBox1.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scComboBoxExt cboFar;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboEdo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.ListView lstPedidos;
        private System.Windows.Forms.ColumnHeader colFolio;
        private System.Windows.Forms.ColumnHeader IdProveedor;
        private System.Windows.Forms.ColumnHeader colProveedor;
        private System.Windows.Forms.ColumnHeader colIdStatusOrden;
        private System.Windows.Forms.ColumnHeader StatusOrden;
        private System.Windows.Forms.ColumnHeader IdEmpresa;
        private System.Windows.Forms.ColumnHeader IdEstado;
        private System.Windows.Forms.ColumnHeader IdFarmacia;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private SC_ControlsCS.scTextBoxExt txtInicial;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtFinal;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem btnSiguiente;
        private System.Windows.Forms.Label lblMensajes;
    }
}