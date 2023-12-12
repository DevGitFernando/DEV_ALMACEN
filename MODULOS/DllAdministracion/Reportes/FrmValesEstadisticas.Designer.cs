namespace DllAdministracion.Reportes
{
    partial class FrmValesEstadisticas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmValesEstadisticas));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.grpUnidad = new System.Windows.Forms.GroupBox();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.numBusqueda = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lstConcentrado_Vales = new SC_ControlsCS.scListView();
            this.tabConcentrado = new System.Windows.Forms.TabControl();
            this.pgConcentrado = new System.Windows.Forms.TabPage();
            this.pgFarmacias = new System.Windows.Forms.TabPage();
            this.lstFarmacias_Vales = new SC_ControlsCS.scListView();
            this.tabClaves = new System.Windows.Forms.TabPage();
            this.lstClaves_Vales = new SC_ControlsCS.scListView();
            this.tabPerdidas = new System.Windows.Forms.TabPage();
            this.lstPerdidas_Vales = new SC_ControlsCS.scListView();
            this.tabProveedores = new System.Windows.Forms.TabPage();
            this.lstProveedores_Vales = new SC_ControlsCS.scListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.grpUnidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBusqueda)).BeginInit();
            this.tabConcentrado.SuspendLayout();
            this.pgConcentrado.SuspendLayout();
            this.pgFarmacias.SuspendLayout();
            this.tabClaves.SuspendLayout();
            this.tabPerdidas.SuspendLayout();
            this.tabProveedores.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(935, 25);
            this.toolStripBarraMenu.TabIndex = 20;
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
            // grpUnidad
            // 
            this.grpUnidad.Controls.Add(this.cboEmpresas);
            this.grpUnidad.Controls.Add(this.label6);
            this.grpUnidad.Controls.Add(this.cboEstados);
            this.grpUnidad.Controls.Add(this.label8);
            this.grpUnidad.Location = new System.Drawing.Point(12, 28);
            this.grpUnidad.Name = "grpUnidad";
            this.grpUnidad.Size = new System.Drawing.Size(669, 81);
            this.grpUnidad.TabIndex = 0;
            this.grpUnidad.TabStop = false;
            this.grpUnidad.Text = "Información de Unidad";
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.FormattingEnabled = true;
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(80, 19);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(566, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(14, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Empresa :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(80, 46);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(566, 21);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(22, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numBusqueda
            // 
            this.numBusqueda.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBusqueda.Location = new System.Drawing.Point(100, 43);
            this.numBusqueda.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numBusqueda.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBusqueda.Name = "numBusqueda";
            this.numBusqueda.Size = new System.Drawing.Size(89, 20);
            this.numBusqueda.TabIndex = 25;
            this.numBusqueda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numBusqueda.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Top Reporte :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(44, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "yyyy-MM";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(100, 16);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(89, 20);
            this.dtpFecha.TabIndex = 2;
            this.dtpFecha.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // lstConcentrado_Vales
            // 
            this.lstConcentrado_Vales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstConcentrado_Vales.Location = new System.Drawing.Point(6, 7);
            this.lstConcentrado_Vales.LockColumnSize = false;
            this.lstConcentrado_Vales.Name = "lstConcentrado_Vales";
            this.lstConcentrado_Vales.ShowItemToolTips = true;
            this.lstConcentrado_Vales.Size = new System.Drawing.Size(895, 353);
            this.lstConcentrado_Vales.TabIndex = 5;
            this.lstConcentrado_Vales.UseCompatibleStateImageBehavior = false;
            // 
            // tabConcentrado
            // 
            this.tabConcentrado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabConcentrado.Controls.Add(this.pgConcentrado);
            this.tabConcentrado.Controls.Add(this.pgFarmacias);
            this.tabConcentrado.Controls.Add(this.tabClaves);
            this.tabConcentrado.Controls.Add(this.tabPerdidas);
            this.tabConcentrado.Controls.Add(this.tabProveedores);
            this.tabConcentrado.Location = new System.Drawing.Point(12, 115);
            this.tabConcentrado.Name = "tabConcentrado";
            this.tabConcentrado.SelectedIndex = 0;
            this.tabConcentrado.Size = new System.Drawing.Size(915, 390);
            this.tabConcentrado.TabIndex = 22;
            // 
            // pgConcentrado
            // 
            this.pgConcentrado.Controls.Add(this.lstConcentrado_Vales);
            this.pgConcentrado.Location = new System.Drawing.Point(4, 22);
            this.pgConcentrado.Name = "pgConcentrado";
            this.pgConcentrado.Padding = new System.Windows.Forms.Padding(3);
            this.pgConcentrado.Size = new System.Drawing.Size(907, 364);
            this.pgConcentrado.TabIndex = 0;
            this.pgConcentrado.Text = "Concentrado del Mes";
            this.pgConcentrado.UseVisualStyleBackColor = true;
            // 
            // pgFarmacias
            // 
            this.pgFarmacias.Controls.Add(this.lstFarmacias_Vales);
            this.pgFarmacias.Location = new System.Drawing.Point(4, 22);
            this.pgFarmacias.Name = "pgFarmacias";
            this.pgFarmacias.Padding = new System.Windows.Forms.Padding(3);
            this.pgFarmacias.Size = new System.Drawing.Size(907, 364);
            this.pgFarmacias.TabIndex = 1;
            this.pgFarmacias.Text = "Top Farmacias que emiten mas vales";
            this.pgFarmacias.UseVisualStyleBackColor = true;
            // 
            // lstFarmacias_Vales
            // 
            this.lstFarmacias_Vales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstFarmacias_Vales.Location = new System.Drawing.Point(6, 6);
            this.lstFarmacias_Vales.LockColumnSize = false;
            this.lstFarmacias_Vales.Name = "lstFarmacias_Vales";
            this.lstFarmacias_Vales.ShowItemToolTips = true;
            this.lstFarmacias_Vales.Size = new System.Drawing.Size(895, 353);
            this.lstFarmacias_Vales.TabIndex = 6;
            this.lstFarmacias_Vales.UseCompatibleStateImageBehavior = false;
            // 
            // tabClaves
            // 
            this.tabClaves.Controls.Add(this.lstClaves_Vales);
            this.tabClaves.Location = new System.Drawing.Point(4, 22);
            this.tabClaves.Name = "tabClaves";
            this.tabClaves.Padding = new System.Windows.Forms.Padding(3);
            this.tabClaves.Size = new System.Drawing.Size(907, 364);
            this.tabClaves.TabIndex = 2;
            this.tabClaves.Text = "Top Claves que emiten mas vales";
            this.tabClaves.UseVisualStyleBackColor = true;
            // 
            // lstClaves_Vales
            // 
            this.lstClaves_Vales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstClaves_Vales.Location = new System.Drawing.Point(6, 6);
            this.lstClaves_Vales.LockColumnSize = false;
            this.lstClaves_Vales.Name = "lstClaves_Vales";
            this.lstClaves_Vales.ShowItemToolTips = true;
            this.lstClaves_Vales.Size = new System.Drawing.Size(895, 353);
            this.lstClaves_Vales.TabIndex = 7;
            this.lstClaves_Vales.UseCompatibleStateImageBehavior = false;
            // 
            // tabPerdidas
            // 
            this.tabPerdidas.Controls.Add(this.lstPerdidas_Vales);
            this.tabPerdidas.Location = new System.Drawing.Point(4, 22);
            this.tabPerdidas.Name = "tabPerdidas";
            this.tabPerdidas.Padding = new System.Windows.Forms.Padding(3);
            this.tabPerdidas.Size = new System.Drawing.Size(907, 364);
            this.tabPerdidas.TabIndex = 3;
            this.tabPerdidas.Text = "Claves con Perdidas";
            this.tabPerdidas.UseVisualStyleBackColor = true;
            // 
            // lstPerdidas_Vales
            // 
            this.lstPerdidas_Vales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstPerdidas_Vales.Location = new System.Drawing.Point(6, 6);
            this.lstPerdidas_Vales.LockColumnSize = false;
            this.lstPerdidas_Vales.Name = "lstPerdidas_Vales";
            this.lstPerdidas_Vales.ShowItemToolTips = true;
            this.lstPerdidas_Vales.Size = new System.Drawing.Size(895, 353);
            this.lstPerdidas_Vales.TabIndex = 0;
            this.lstPerdidas_Vales.UseCompatibleStateImageBehavior = false;
            // 
            // tabProveedores
            // 
            this.tabProveedores.Controls.Add(this.lstProveedores_Vales);
            this.tabProveedores.Location = new System.Drawing.Point(4, 22);
            this.tabProveedores.Name = "tabProveedores";
            this.tabProveedores.Padding = new System.Windows.Forms.Padding(3);
            this.tabProveedores.Size = new System.Drawing.Size(907, 364);
            this.tabProveedores.TabIndex = 4;
            this.tabProveedores.Text = "Importes Registrados por Proveedor";
            this.tabProveedores.UseVisualStyleBackColor = true;
            // 
            // lstProveedores_Vales
            // 
            this.lstProveedores_Vales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstProveedores_Vales.Location = new System.Drawing.Point(6, 5);
            this.lstProveedores_Vales.LockColumnSize = false;
            this.lstProveedores_Vales.Name = "lstProveedores_Vales";
            this.lstProveedores_Vales.ShowItemToolTips = true;
            this.lstProveedores_Vales.Size = new System.Drawing.Size(895, 353);
            this.lstProveedores_Vales.TabIndex = 1;
            this.lstProveedores_Vales.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numBusqueda);
            this.groupBox1.Controls.Add(this.dtpFecha);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(687, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 81);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametros";
            // 
            // FrmValesEstadisticas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 511);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabConcentrado);
            this.Controls.Add(this.grpUnidad);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmValesEstadisticas";
            this.Text = "Estadistica de control de Vales de insumos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmValesEstadisticas_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.grpUnidad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numBusqueda)).EndInit();
            this.tabConcentrado.ResumeLayout(false);
            this.pgConcentrado.ResumeLayout(false);
            this.pgFarmacias.ResumeLayout(false);
            this.tabClaves.ResumeLayout(false);
            this.tabPerdidas.ResumeLayout(false);
            this.tabProveedores.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox grpUnidad;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private SC_ControlsCS.scListView lstConcentrado_Vales;
        private System.Windows.Forms.TabControl tabConcentrado;
        private System.Windows.Forms.TabPage pgConcentrado;
        private System.Windows.Forms.TabPage pgFarmacias;
        private SC_ControlsCS.scListView lstFarmacias_Vales;
        private System.Windows.Forms.TabPage tabClaves;
        private SC_ControlsCS.scListView lstClaves_Vales;
        private System.Windows.Forms.TabPage tabPerdidas;
        private SC_ControlsCS.scListView lstPerdidas_Vales;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numBusqueda;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabProveedores;
        private SC_ControlsCS.scListView lstProveedores_Vales;
    }
}