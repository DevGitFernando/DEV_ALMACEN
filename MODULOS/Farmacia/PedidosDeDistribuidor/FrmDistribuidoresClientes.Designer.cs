namespace Farmacia.PedidosDeDistribuidor
{
    partial class FrmDistribuidoresClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDistribuidoresClientes));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDistribuidor = new System.Windows.Forms.Label();
            this.txtDistribuidor = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtCodigoCliente = new SC_ControlsCS.scTextBoxExt();
            this.txtNombreCliente = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(590, 25);
            this.toolStripBarraMenu.TabIndex = 4;
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar";
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
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
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
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDistribuidor);
            this.groupBox1.Controls.Add(this.txtDistribuidor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboFarmacias);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtCodigoCliente);
            this.groupBox1.Controls.Add(this.txtNombreCliente);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(11, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 137);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Medico:";
            // 
            // lblDistribuidor
            // 
            this.lblDistribuidor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDistribuidor.Location = new System.Drawing.Point(179, 21);
            this.lblDistribuidor.Name = "lblDistribuidor";
            this.lblDistribuidor.Size = new System.Drawing.Size(366, 21);
            this.lblDistribuidor.TabIndex = 14;
            this.lblDistribuidor.Text = "Distribuidor :";
            this.lblDistribuidor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDistribuidor
            // 
            this.txtDistribuidor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDistribuidor.Decimales = 0;
            this.txtDistribuidor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtDistribuidor.ForeColor = System.Drawing.Color.Black;
            this.txtDistribuidor.Location = new System.Drawing.Point(115, 21);
            this.txtDistribuidor.MaxLength = 6;
            this.txtDistribuidor.Name = "txtDistribuidor";
            this.txtDistribuidor.PermitirApostrofo = false;
            this.txtDistribuidor.PermitirNegativos = false;
            this.txtDistribuidor.Size = new System.Drawing.Size(58, 20);
            this.txtDistribuidor.TabIndex = 0;
            this.txtDistribuidor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDistribuidor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDistribuidor_KeyDown);
            this.txtDistribuidor.Validating += new System.ComponentModel.CancelEventHandler(this.txtDistribuidor_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(42, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Distribuidor :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(115, 101);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(430, 21);
            this.cboFarmacias.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Unidad :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(235, 47);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(81, 13);
            this.lblCancelado.TabIndex = 4;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.Visible = false;
            // 
            // txtCodigoCliente
            // 
            this.txtCodigoCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoCliente.Decimales = 2;
            this.txtCodigoCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoCliente.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoCliente.Location = new System.Drawing.Point(115, 47);
            this.txtCodigoCliente.MaxLength = 8;
            this.txtCodigoCliente.Name = "txtCodigoCliente";
            this.txtCodigoCliente.PermitirApostrofo = false;
            this.txtCodigoCliente.PermitirNegativos = false;
            this.txtCodigoCliente.Size = new System.Drawing.Size(105, 20);
            this.txtCodigoCliente.TabIndex = 1;
            this.txtCodigoCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoCliente_KeyDown);
            this.txtCodigoCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoCliente_Validating);
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombreCliente.Decimales = 2;
            this.txtNombreCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombreCliente.ForeColor = System.Drawing.Color.Black;
            this.txtNombreCliente.Location = new System.Drawing.Point(115, 74);
            this.txtNombreCliente.MaxLength = 50;
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.PermitirApostrofo = false;
            this.txtNombreCliente.PermitirNegativos = false;
            this.txtNombreCliente.Size = new System.Drawing.Size(430, 20);
            this.txtNombreCliente.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Codigo Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre Cliente :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmDistribuidoresClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 178);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmDistribuidoresClientes";
            this.Text = "Registro de Clientes - Distribuidor";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmMedicos_Load);
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
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtCodigoCliente;
        private SC_ControlsCS.scTextBoxExt txtNombreCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private SC_ControlsCS.scTextBoxExt txtDistribuidor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDistribuidor;
    }
}