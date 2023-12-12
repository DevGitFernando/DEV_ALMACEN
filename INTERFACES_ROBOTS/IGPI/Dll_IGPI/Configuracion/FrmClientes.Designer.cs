namespace Dll_IGPI.Configuracion
{
    partial class FrmClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClientes));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtIdEstado = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFarmacia = new System.Windows.Forms.Label();
            this.txtIdCliente = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIdFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.tmIGPI = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(492, 25);
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblEstado);
            this.groupBox1.Controls.Add(this.txtIdEstado);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblFarmacia);
            this.groupBox1.Controls.Add(this.txtIdCliente);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtIdFarmacia);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 96);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales";
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstado.Location = new System.Drawing.Point(138, 42);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(321, 20);
            this.lblEstado.TabIndex = 9;
            this.lblEstado.Text = "Farmacia :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdEstado
            // 
            this.txtIdEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdEstado.Decimales = 2;
            this.txtIdEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdEstado.ForeColor = System.Drawing.Color.Black;
            this.txtIdEstado.Location = new System.Drawing.Point(71, 42);
            this.txtIdEstado.MaxLength = 3;
            this.txtIdEstado.Name = "txtIdEstado";
            this.txtIdEstado.PermitirApostrofo = false;
            this.txtIdEstado.PermitirNegativos = false;
            this.txtIdEstado.Size = new System.Drawing.Size(61, 20);
            this.txtIdEstado.TabIndex = 1;
            this.txtIdEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdEstado.TextChanged += new System.EventHandler(this.txtIdEstado_TextChanged);
            this.txtIdEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdEstado_KeyDown);
            this.txtIdEstado.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdEstado_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Estado :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(138, 65);
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(321, 20);
            this.lblFarmacia.TabIndex = 6;
            this.lblFarmacia.Text = "Farmacia :";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdCliente.Decimales = 2;
            this.txtIdCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdCliente.ForeColor = System.Drawing.Color.Black;
            this.txtIdCliente.Location = new System.Drawing.Point(71, 19);
            this.txtIdCliente.MaxLength = 3;
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.PermitirApostrofo = false;
            this.txtIdCliente.PermitirNegativos = false;
            this.txtIdCliente.Size = new System.Drawing.Size(61, 20);
            this.txtIdCliente.TabIndex = 0;
            this.txtIdCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdCliente_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdFarmacia
            // 
            this.txtIdFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFarmacia.Decimales = 2;
            this.txtIdFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtIdFarmacia.Location = new System.Drawing.Point(71, 65);
            this.txtIdFarmacia.MaxLength = 3;
            this.txtIdFarmacia.Name = "txtIdFarmacia";
            this.txtIdFarmacia.PermitirApostrofo = false;
            this.txtIdFarmacia.PermitirNegativos = false;
            this.txtIdFarmacia.Size = new System.Drawing.Size(61, 20);
            this.txtIdFarmacia.TabIndex = 2;
            this.txtIdFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFarmacia.TextChanged += new System.EventHandler(this.txtIdFarmacia_TextChanged);
            this.txtIdFarmacia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdFarmacia_KeyDown);
            this.txtIdFarmacia.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFarmacia_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Farmacia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmIGPI
            // 
            this.tmIGPI.Interval = 500;
            this.tmIGPI.Tick += new System.EventHandler(this.tmIGPI_Tick);
            // 
            // FrmClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 135);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmClientes";
            this.Text = "Relación de Clientes y Farmacias";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmClientes_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtIdCliente;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtIdFarmacia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFarmacia;
        private System.Windows.Forms.Timer tmIGPI;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scTextBoxExt txtIdEstado;
        private System.Windows.Forms.Label label4;
    }
}