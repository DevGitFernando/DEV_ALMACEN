namespace Compras_Cuentas_x_Pagar.Registros
{
    partial class FrmEstadoDeCuenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstadoDeCuenta));
            this.FrameDatosGenerales = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoLiquidado = new System.Windows.Forms.RadioButton();
            this.rdoConSaldo = new System.Windows.Forms.RadioButton();
            this.RdoTodos = new System.Windows.Forms.RadioButton();
            this.FrameOrigenDatos = new System.Windows.Forms.GroupBox();
            this.rdoAmbos = new System.Windows.Forms.RadioButton();
            this.rdoCentral = new System.Windows.Forms.RadioButton();
            this.rdoRegional = new System.Windows.Forms.RadioButton();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtIdEstado = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.txtIdProveedor = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStriplblResultado = new System.Windows.Forms.ToolStripLabel();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameDatosGenerales.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameOrigenDatos.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameDatosGenerales
            // 
            this.FrameDatosGenerales.Controls.Add(this.groupBox1);
            this.FrameDatosGenerales.Controls.Add(this.FrameOrigenDatos);
            this.FrameDatosGenerales.Controls.Add(this.lblEstado);
            this.FrameDatosGenerales.Controls.Add(this.txtIdEstado);
            this.FrameDatosGenerales.Controls.Add(this.label3);
            this.FrameDatosGenerales.Controls.Add(this.lblProveedor);
            this.FrameDatosGenerales.Controls.Add(this.txtIdProveedor);
            this.FrameDatosGenerales.Controls.Add(this.label2);
            this.FrameDatosGenerales.Location = new System.Drawing.Point(16, 34);
            this.FrameDatosGenerales.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatosGenerales.Name = "FrameDatosGenerales";
            this.FrameDatosGenerales.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatosGenerales.Size = new System.Drawing.Size(757, 150);
            this.FrameDatosGenerales.TabIndex = 0;
            this.FrameDatosGenerales.TabStop = false;
            this.FrameDatosGenerales.Text = "Datos Generales";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoLiquidado);
            this.groupBox1.Controls.Add(this.rdoConSaldo);
            this.groupBox1.Controls.Add(this.RdoTodos);
            this.groupBox1.Location = new System.Drawing.Point(400, 85);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(344, 57);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // rdoLiquidado
            // 
            this.rdoLiquidado.Location = new System.Drawing.Point(131, 23);
            this.rdoLiquidado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoLiquidado.Name = "rdoLiquidado";
            this.rdoLiquidado.Size = new System.Drawing.Size(95, 21);
            this.rdoLiquidado.TabIndex = 1;
            this.rdoLiquidado.TabStop = true;
            this.rdoLiquidado.Text = "Liquidado";
            this.rdoLiquidado.UseVisualStyleBackColor = true;
            // 
            // rdoConSaldo
            // 
            this.rdoConSaldo.Location = new System.Drawing.Point(21, 23);
            this.rdoConSaldo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoConSaldo.Name = "rdoConSaldo";
            this.rdoConSaldo.Size = new System.Drawing.Size(100, 21);
            this.rdoConSaldo.TabIndex = 0;
            this.rdoConSaldo.TabStop = true;
            this.rdoConSaldo.Text = "Con Saldo";
            this.rdoConSaldo.UseVisualStyleBackColor = true;
            // 
            // RdoTodos
            // 
            this.RdoTodos.Location = new System.Drawing.Point(248, 23);
            this.RdoTodos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RdoTodos.Name = "RdoTodos";
            this.RdoTodos.Size = new System.Drawing.Size(76, 21);
            this.RdoTodos.TabIndex = 2;
            this.RdoTodos.TabStop = true;
            this.RdoTodos.Text = "Ambos";
            this.RdoTodos.UseVisualStyleBackColor = true;
            // 
            // FrameOrigenDatos
            // 
            this.FrameOrigenDatos.Controls.Add(this.rdoAmbos);
            this.FrameOrigenDatos.Controls.Add(this.rdoCentral);
            this.FrameOrigenDatos.Controls.Add(this.rdoRegional);
            this.FrameOrigenDatos.Location = new System.Drawing.Point(44, 85);
            this.FrameOrigenDatos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameOrigenDatos.Name = "FrameOrigenDatos";
            this.FrameOrigenDatos.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameOrigenDatos.Size = new System.Drawing.Size(344, 57);
            this.FrameOrigenDatos.TabIndex = 2;
            this.FrameOrigenDatos.TabStop = false;
            this.FrameOrigenDatos.Text = "Origen de Órdenes de Compra";
            // 
            // rdoAmbos
            // 
            this.rdoAmbos.Location = new System.Drawing.Point(39, 23);
            this.rdoAmbos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoAmbos.Name = "rdoAmbos";
            this.rdoAmbos.Size = new System.Drawing.Size(79, 21);
            this.rdoAmbos.TabIndex = 0;
            this.rdoAmbos.TabStop = true;
            this.rdoAmbos.Text = "Ambos";
            this.rdoAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoCentral
            // 
            this.rdoCentral.Location = new System.Drawing.Point(133, 23);
            this.rdoCentral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoCentral.Name = "rdoCentral";
            this.rdoCentral.Size = new System.Drawing.Size(91, 21);
            this.rdoCentral.TabIndex = 1;
            this.rdoCentral.TabStop = true;
            this.rdoCentral.Text = "Central";
            this.rdoCentral.UseVisualStyleBackColor = true;
            // 
            // rdoRegional
            // 
            this.rdoRegional.Location = new System.Drawing.Point(240, 23);
            this.rdoRegional.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoRegional.Name = "rdoRegional";
            this.rdoRegional.Size = new System.Drawing.Size(91, 21);
            this.rdoRegional.TabIndex = 2;
            this.rdoRegional.TabStop = true;
            this.rdoRegional.Text = "Regional";
            this.rdoRegional.UseVisualStyleBackColor = true;
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstado.Location = new System.Drawing.Point(269, 53);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(475, 26);
            this.lblEstado.TabIndex = 48;
            this.lblEstado.Text = "Proveedor :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdEstado
            // 
            this.txtIdEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdEstado.Decimales = 2;
            this.txtIdEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdEstado.ForeColor = System.Drawing.Color.Black;
            this.txtIdEstado.Location = new System.Drawing.Point(129, 53);
            this.txtIdEstado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIdEstado.MaxLength = 4;
            this.txtIdEstado.Name = "txtIdEstado";
            this.txtIdEstado.PermitirApostrofo = false;
            this.txtIdEstado.PermitirNegativos = false;
            this.txtIdEstado.Size = new System.Drawing.Size(132, 22);
            this.txtIdEstado.TabIndex = 1;
            this.txtIdEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdEstado.TextChanged += new System.EventHandler(this.txtIdEstado_TextChanged);
            this.txtIdEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdEstado_KeyDown);
            this.txtIdEstado.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdEstado_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 25);
            this.label3.TabIndex = 47;
            this.label3.Text = "Estado :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProveedor
            // 
            this.lblProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProveedor.Location = new System.Drawing.Point(269, 21);
            this.lblProveedor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(475, 26);
            this.lblProveedor.TabIndex = 45;
            this.lblProveedor.Text = "Proveedor :";
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdProveedor
            // 
            this.txtIdProveedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProveedor.Decimales = 2;
            this.txtIdProveedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdProveedor.ForeColor = System.Drawing.Color.Black;
            this.txtIdProveedor.Location = new System.Drawing.Point(129, 21);
            this.txtIdProveedor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIdProveedor.MaxLength = 4;
            this.txtIdProveedor.Name = "txtIdProveedor";
            this.txtIdProveedor.PermitirApostrofo = false;
            this.txtIdProveedor.PermitirNegativos = false;
            this.txtIdProveedor.Size = new System.Drawing.Size(132, 22);
            this.txtIdProveedor.TabIndex = 0;
            this.txtIdProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdProveedor.TextChanged += new System.EventHandler(this.txtIdProveedor_TextChanged);
            this.txtIdProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdProveedor_KeyDown);
            this.txtIdProveedor.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdProveedor_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 25);
            this.label2.TabIndex = 44;
            this.label2.Text = "Proveedor :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.toolStriplblResultado,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(785, 25);
            this.toolStripBarraMenu.TabIndex = 3;
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
            // toolStriplblResultado
            // 
            this.toolStriplblResultado.Name = "toolStriplblResultado";
            this.toolStriplblResultado.Size = new System.Drawing.Size(0, 22);
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
            // FrmEstadoDeCuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 194);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameDatosGenerales);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmEstadoDeCuenta";
            this.Text = "Estado de cuenta";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEstadoDeCuenta_Load);
            this.FrameDatosGenerales.ResumeLayout(false);
            this.FrameDatosGenerales.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.FrameOrigenDatos.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameDatosGenerales;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoLiquidado;
        private System.Windows.Forms.RadioButton rdoConSaldo;
        private System.Windows.Forms.RadioButton RdoTodos;
        private System.Windows.Forms.GroupBox FrameOrigenDatos;
        private System.Windows.Forms.RadioButton rdoAmbos;
        private System.Windows.Forms.RadioButton rdoCentral;
        private System.Windows.Forms.RadioButton rdoRegional;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scTextBoxExt txtIdEstado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProveedor;
        private SC_ControlsCS.scTextBoxExt txtIdProveedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripLabel toolStriplblResultado;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
    }
}