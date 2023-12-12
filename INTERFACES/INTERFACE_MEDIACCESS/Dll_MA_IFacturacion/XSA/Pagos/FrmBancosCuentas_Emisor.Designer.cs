namespace Dll_MA_IFacturacion.XSA
{
    partial class FrmBancosCuentas_Emisor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBancosCuentas_Emisor));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBanco = new SC_ControlsCS.scLabelExt();
            this.txtNumeroDeCuenta = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRFC_Banco = new SC_ControlsCS.scTextBoxExt();
            this.lblRFC_Banco = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.lblCliente = new SC_ControlsCS.scLabelExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtRFC_Emisor = new SC_ControlsCS.scTextBoxExt();
            this.lblRFC_Emisor = new System.Windows.Forms.Label();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(702, 25);
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
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBanco);
            this.groupBox1.Controls.Add(this.txtNumeroDeCuenta);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRFC_Banco);
            this.groupBox1.Controls.Add(this.lblRFC_Banco);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtRFC_Emisor);
            this.groupBox1.Controls.Add(this.lblRFC_Emisor);
            this.groupBox1.Location = new System.Drawing.Point(15, 31);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(675, 212);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // lblBanco
            // 
            this.lblBanco.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBanco.Location = new System.Drawing.Point(341, 141);
            this.lblBanco.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBanco.MostrarToolTip = false;
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(318, 22);
            this.lblBanco.TabIndex = 21;
            this.lblBanco.Text = "scLabelExt1";
            this.lblBanco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNumeroDeCuenta
            // 
            this.txtNumeroDeCuenta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroDeCuenta.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumeroDeCuenta.Decimales = 2;
            this.txtNumeroDeCuenta.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumeroDeCuenta.ForeColor = System.Drawing.Color.Black;
            this.txtNumeroDeCuenta.Location = new System.Drawing.Point(157, 171);
            this.txtNumeroDeCuenta.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumeroDeCuenta.MaxLength = 50;
            this.txtNumeroDeCuenta.Name = "txtNumeroDeCuenta";
            this.txtNumeroDeCuenta.PermitirApostrofo = false;
            this.txtNumeroDeCuenta.PermitirNegativos = false;
            this.txtNumeroDeCuenta.Size = new System.Drawing.Size(502, 22);
            this.txtNumeroDeCuenta.TabIndex = 3;
            this.txtNumeroDeCuenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNumeroDeCuenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumeroDeCuenta_KeyDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 174);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Número De Cuenta :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRFC_Banco
            // 
            this.txtRFC_Banco.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRFC_Banco.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRFC_Banco.Decimales = 2;
            this.txtRFC_Banco.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRFC_Banco.ForeColor = System.Drawing.Color.Black;
            this.txtRFC_Banco.Location = new System.Drawing.Point(157, 141);
            this.txtRFC_Banco.Margin = new System.Windows.Forms.Padding(4);
            this.txtRFC_Banco.MaxLength = 15;
            this.txtRFC_Banco.Name = "txtRFC_Banco";
            this.txtRFC_Banco.PermitirApostrofo = false;
            this.txtRFC_Banco.PermitirNegativos = false;
            this.txtRFC_Banco.Size = new System.Drawing.Size(176, 22);
            this.txtRFC_Banco.TabIndex = 2;
            this.txtRFC_Banco.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRFC_Banco.TextChanged += new System.EventHandler(this.txtRFC_Banco_TextChanged);
            this.txtRFC_Banco.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRFC_Banco_KeyDown);
            this.txtRFC_Banco.Validating += new System.ComponentModel.CancelEventHandler(this.txtRFC_Banco_Validating);
            // 
            // lblRFC_Banco
            // 
            this.lblRFC_Banco.Location = new System.Drawing.Point(13, 144);
            this.lblRFC_Banco.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRFC_Banco.Name = "lblRFC_Banco";
            this.lblRFC_Banco.Size = new System.Drawing.Size(138, 16);
            this.lblRFC_Banco.TabIndex = 18;
            this.lblRFC_Banco.Text = "RFC Banco :";
            this.lblRFC_Banco.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(157, 20);
            this.txtId.Margin = new System.Windows.Forms.Padding(4);
            this.txtId.MaxLength = 6;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(162, 22);
            this.txtId.TabIndex = 0;
            this.txtId.Text = "123456";
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(157, 47);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCliente.MostrarToolTip = false;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(502, 57);
            this.lblCliente.TabIndex = 15;
            this.lblCliente.Text = "scLabelExt1";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 19);
            this.label3.TabIndex = 14;
            this.label3.Text = "Clave :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(327, 23);
            this.lblCancelado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(133, 16);
            this.lblCancelado.TabIndex = 4;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtRFC_Emisor
            // 
            this.txtRFC_Emisor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRFC_Emisor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRFC_Emisor.Decimales = 2;
            this.txtRFC_Emisor.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRFC_Emisor.ForeColor = System.Drawing.Color.Black;
            this.txtRFC_Emisor.Location = new System.Drawing.Point(157, 111);
            this.txtRFC_Emisor.Margin = new System.Windows.Forms.Padding(4);
            this.txtRFC_Emisor.MaxLength = 15;
            this.txtRFC_Emisor.Name = "txtRFC_Emisor";
            this.txtRFC_Emisor.PermitirApostrofo = false;
            this.txtRFC_Emisor.PermitirNegativos = false;
            this.txtRFC_Emisor.Size = new System.Drawing.Size(176, 22);
            this.txtRFC_Emisor.TabIndex = 1;
            this.txtRFC_Emisor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblRFC_Emisor
            // 
            this.lblRFC_Emisor.Location = new System.Drawing.Point(13, 114);
            this.lblRFC_Emisor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRFC_Emisor.Name = "lblRFC_Emisor";
            this.lblRFC_Emisor.Size = new System.Drawing.Size(138, 16);
            this.lblRFC_Emisor.TabIndex = 2;
            this.lblRFC_Emisor.Text = "RFC Emisor :";
            this.lblRFC_Emisor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmBancosCuentas_Emisor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 251);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmBancosCuentas_Emisor";
            this.Text = "Catálogos de cuentas de Emisores";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFACT_BancosCuentas_Emisor_Load);
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
        private SC_ControlsCS.scTextBoxExt txtRFC_Emisor;
        private System.Windows.Forms.Label lblRFC_Emisor;
        private SC_ControlsCS.scLabelExt lblCliente;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtId;
        private SC_ControlsCS.scTextBoxExt txtRFC_Banco;
        private System.Windows.Forms.Label lblRFC_Banco;
        private SC_ControlsCS.scTextBoxExt txtNumeroDeCuenta;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scLabelExt lblBanco;
    }
}