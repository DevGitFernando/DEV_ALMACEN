namespace Dll_IFacturacion.XSA
{
    partial class FrmConceptos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConceptos));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescripcion = new SC_ControlsCS.scTextBoxExt();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblProductoSAT = new System.Windows.Forms.Label();
            this.txtProductoSAT = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblUnidadMedidaSAT = new System.Windows.Forms.Label();
            this.txtUnidadMedidaSAT = new SC_ControlsCS.scTextBoxExt();
            this.label13 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(840, 25);
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
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(16, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(809, 274);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Concepto";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDescripcion.Decimales = 2;
            this.txtDescripcion.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDescripcion.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Location = new System.Drawing.Point(156, 60);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescripcion.MaxLength = 500;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.PermitirApostrofo = false;
            this.txtDescripcion.PermitirNegativos = false;
            this.txtDescripcion.Size = new System.Drawing.Size(636, 198);
            this.txtDescripcion.TabIndex = 1;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(660, 32);
            this.lblCancelado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(133, 16);
            this.lblCancelado.TabIndex = 4;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtId
            // 
            this.txtId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(156, 27);
            this.txtId.Margin = new System.Windows.Forms.Padding(4);
            this.txtId.MaxLength = 20;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(176, 22);
            this.txtId.TabIndex = 0;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Concepto :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Descripción :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblProductoSAT);
            this.groupBox3.Controls.Add(this.txtProductoSAT);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lblUnidadMedidaSAT);
            this.groupBox3.Controls.Add(this.txtUnidadMedidaSAT);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(16, 311);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(809, 94);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos SAT";
            // 
            // lblProductoSAT
            // 
            this.lblProductoSAT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProductoSAT.Location = new System.Drawing.Point(320, 15);
            this.lblProductoSAT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductoSAT.Name = "lblProductoSAT";
            this.lblProductoSAT.Size = new System.Drawing.Size(472, 26);
            this.lblProductoSAT.TabIndex = 43;
            this.lblProductoSAT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProductoSAT
            // 
            this.txtProductoSAT.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtProductoSAT.Decimales = 2;
            this.txtProductoSAT.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtProductoSAT.ForeColor = System.Drawing.Color.Black;
            this.txtProductoSAT.Location = new System.Drawing.Point(151, 17);
            this.txtProductoSAT.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductoSAT.MaxLength = 8;
            this.txtProductoSAT.Name = "txtProductoSAT";
            this.txtProductoSAT.PermitirApostrofo = false;
            this.txtProductoSAT.PermitirNegativos = false;
            this.txtProductoSAT.Size = new System.Drawing.Size(161, 22);
            this.txtProductoSAT.TabIndex = 0;
            this.txtProductoSAT.TextChanged += new System.EventHandler(this.txtProductoSAT_TextChanged);
            this.txtProductoSAT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductoSAT_KeyDown);
            this.txtProductoSAT.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductoSAT_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 18);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 20);
            this.label7.TabIndex = 42;
            this.label7.Text = "Producto :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUnidadMedidaSAT
            // 
            this.lblUnidadMedidaSAT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUnidadMedidaSAT.Location = new System.Drawing.Point(320, 48);
            this.lblUnidadMedidaSAT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUnidadMedidaSAT.Name = "lblUnidadMedidaSAT";
            this.lblUnidadMedidaSAT.Size = new System.Drawing.Size(472, 26);
            this.lblUnidadMedidaSAT.TabIndex = 40;
            this.lblUnidadMedidaSAT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUnidadMedidaSAT
            // 
            this.txtUnidadMedidaSAT.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtUnidadMedidaSAT.Decimales = 2;
            this.txtUnidadMedidaSAT.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtUnidadMedidaSAT.ForeColor = System.Drawing.Color.Black;
            this.txtUnidadMedidaSAT.Location = new System.Drawing.Point(151, 50);
            this.txtUnidadMedidaSAT.Margin = new System.Windows.Forms.Padding(4);
            this.txtUnidadMedidaSAT.MaxLength = 4;
            this.txtUnidadMedidaSAT.Name = "txtUnidadMedidaSAT";
            this.txtUnidadMedidaSAT.PermitirApostrofo = false;
            this.txtUnidadMedidaSAT.PermitirNegativos = false;
            this.txtUnidadMedidaSAT.Size = new System.Drawing.Size(161, 22);
            this.txtUnidadMedidaSAT.TabIndex = 1;
            this.txtUnidadMedidaSAT.TextChanged += new System.EventHandler(this.txtUnidadMedidaSAT_TextChanged);
            this.txtUnidadMedidaSAT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnidadMedidaSAT_KeyDown);
            this.txtUnidadMedidaSAT.Validating += new System.ComponentModel.CancelEventHandler(this.txtUnidadMedidaSAT_Validating);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(16, 51);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(131, 20);
            this.label13.TabIndex = 39;
            this.label13.Text = "Unidad de medida :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmConceptos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 417);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmConceptos";
            this.Text = "Catálogo de Conceptos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConceptos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtDescripcion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblProductoSAT;
        private SC_ControlsCS.scTextBoxExt txtProductoSAT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblUnidadMedidaSAT;
        private SC_ControlsCS.scTextBoxExt txtUnidadMedidaSAT;
        private System.Windows.Forms.Label label13;
    }
}