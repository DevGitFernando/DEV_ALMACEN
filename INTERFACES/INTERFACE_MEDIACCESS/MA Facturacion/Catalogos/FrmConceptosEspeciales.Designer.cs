namespace MA_Facturacion.Catalogos
{
    partial class FrmConceptosEspeciales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConceptosEspeciales));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FramePrincipal = new System.Windows.Forms.GroupBox();
            this.txtConcepto = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameListaDeConceptos = new System.Windows.Forms.GroupBox();
            this.lstConceptos = new System.Windows.Forms.ListView();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBarraMenu.SuspendLayout();
            this.FramePrincipal.SuspendLayout();
            this.FrameListaDeConceptos.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator3,
            this.btnCancelar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(641, 25);
            this.toolStripBarraMenu.TabIndex = 1;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "Cancelar remisión completa";
            this.btnCancelar.ToolTipText = "Cancelar remisión completa";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelarRemision_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FramePrincipal
            // 
            this.FramePrincipal.Controls.Add(this.txtConcepto);
            this.FramePrincipal.Controls.Add(this.label2);
            this.FramePrincipal.Controls.Add(this.cboEstados);
            this.FramePrincipal.Controls.Add(this.label8);
            this.FramePrincipal.Controls.Add(this.lblCancelado);
            this.FramePrincipal.Controls.Add(this.txtFolio);
            this.FramePrincipal.Controls.Add(this.label1);
            this.FramePrincipal.Location = new System.Drawing.Point(12, 28);
            this.FramePrincipal.Name = "FramePrincipal";
            this.FramePrincipal.Size = new System.Drawing.Size(621, 147);
            this.FramePrincipal.TabIndex = 2;
            this.FramePrincipal.TabStop = false;
            this.FramePrincipal.Text = "Datos Generales de Financiamiento";
            // 
            // txtConcepto
            // 
            this.txtConcepto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtConcepto.Decimales = 2;
            this.txtConcepto.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtConcepto.ForeColor = System.Drawing.Color.Black;
            this.txtConcepto.Location = new System.Drawing.Point(91, 71);
            this.txtConcepto.MaxLength = 200;
            this.txtConcepto.Multiline = true;
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.PermitirApostrofo = false;
            this.txtConcepto.PermitirNegativos = false;
            this.txtConcepto.Size = new System.Drawing.Size(521, 65);
            this.txtConcepto.TabIndex = 44;
            this.txtConcepto.TextChanged += new System.EventHandler(this.txtConcepto_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 12);
            this.label2.TabIndex = 43;
            this.label2.Text = "Descripción :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(91, 21);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(521, 21);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(197, 47);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(98, 20);
            this.lblCancelado.TabIndex = 32;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(91, 47);
            this.txtFolio.MaxLength = 4;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.TextChanged += new System.EventHandler(this.txtFolio_TextChanged);
            this.txtFolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolio_KeyDown);
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "Id Concepto :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameListaDeConceptos
            // 
            this.FrameListaDeConceptos.Controls.Add(this.lstConceptos);
            this.FrameListaDeConceptos.Location = new System.Drawing.Point(10, 176);
            this.FrameListaDeConceptos.Name = "FrameListaDeConceptos";
            this.FrameListaDeConceptos.Size = new System.Drawing.Size(622, 286);
            this.FrameListaDeConceptos.TabIndex = 3;
            this.FrameListaDeConceptos.TabStop = false;
            this.FrameListaDeConceptos.Text = "Listado de Conceptos";
            // 
            // lstConceptos
            // 
            this.lstConceptos.Location = new System.Drawing.Point(10, 16);
            this.lstConceptos.Name = "lstConceptos";
            this.lstConceptos.Size = new System.Drawing.Size(604, 260);
            this.lstConceptos.TabIndex = 0;
            this.lstConceptos.UseCompatibleStateImageBehavior = false;
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator.Visible = false;
            // 
            // FrmConceptosEspeciales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 468);
            this.Controls.Add(this.FrameListaDeConceptos);
            this.Controls.Add(this.FramePrincipal);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmConceptosEspeciales";
            this.Text = "Conceptos Especiales para Facturación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConceptosEspeciales_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FramePrincipal.ResumeLayout(false);
            this.FramePrincipal.PerformLayout();
            this.FrameListaDeConceptos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.GroupBox FramePrincipal;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtConcepto;
        private System.Windows.Forms.GroupBox FrameListaDeConceptos;
        private System.Windows.Forms.ListView lstConceptos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    }
}