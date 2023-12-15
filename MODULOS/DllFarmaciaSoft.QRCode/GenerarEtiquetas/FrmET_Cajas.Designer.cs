namespace DllFarmaciaSoft.QRCode.GenerarEtiquetas
{
    partial class FrmET_Cajas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmET_Cajas));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.scTextBoxExt1 = new SC_ControlsCS.scTextBoxExt();
            this.lblStatus = new SC_ControlsCS.scLabelExt();
            this.txtIdCaja = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(469, 58);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(54, 55);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 4);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(54, 55);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.scTextBoxExt1);
            this.FrameDatos.Controls.Add(this.lblStatus);
            this.FrameDatos.Controls.Add(this.txtIdCaja);
            this.FrameDatos.Controls.Add(this.label4);
            this.FrameDatos.Location = new System.Drawing.Point(9, 61);
            this.FrameDatos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatos.Size = new System.Drawing.Size(451, 75);
            this.FrameDatos.TabIndex = 2;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Información";
            // 
            // scTextBoxExt1
            // 
            this.scTextBoxExt1.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.scTextBoxExt1.Decimales = 2;
            this.scTextBoxExt1.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.scTextBoxExt1.ForeColor = System.Drawing.Color.Black;
            this.scTextBoxExt1.Location = new System.Drawing.Point(252, 123);
            this.scTextBoxExt1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scTextBoxExt1.MaxLength = 8;
            this.scTextBoxExt1.Name = "scTextBoxExt1";
            this.scTextBoxExt1.PermitirApostrofo = false;
            this.scTextBoxExt1.PermitirNegativos = false;
            this.scTextBoxExt1.Size = new System.Drawing.Size(121, 22);
            this.scTextBoxExt1.TabIndex = 16;
            this.scTextBoxExt1.Text = "01234567";
            this.scTextBoxExt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(224, 28);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.MostrarToolTip = false;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(194, 25);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdCaja
            // 
            this.txtIdCaja.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdCaja.Decimales = 2;
            this.txtIdCaja.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdCaja.ForeColor = System.Drawing.Color.Black;
            this.txtIdCaja.Location = new System.Drawing.Point(94, 28);
            this.txtIdCaja.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIdCaja.MaxLength = 8;
            this.txtIdCaja.Name = "txtIdCaja";
            this.txtIdCaja.PermitirApostrofo = false;
            this.txtIdCaja.PermitirNegativos = false;
            this.txtIdCaja.Size = new System.Drawing.Size(121, 22);
            this.txtIdCaja.TabIndex = 0;
            this.txtIdCaja.Text = "01234567";
            this.txtIdCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdCaja.TextChanged += new System.EventHandler(this.txtIdCaja_TextChanged);
            this.txtIdCaja.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdCaja_KeyDown);
            this.txtIdCaja.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdCaja_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 23);
            this.label4.TabIndex = 15;
            this.label4.Text = "Id Caja :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmET_Cajas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 147);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmET_Cajas";
            this.ShowIcon = false;
            this.Text = "Etiqueta Cajas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmET_Cajas_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatos.ResumeLayout(false);
            this.FrameDatos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.GroupBox FrameDatos;
        private SC_ControlsCS.scTextBoxExt txtIdCaja;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scLabelExt lblStatus;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private SC_ControlsCS.scTextBoxExt scTextBoxExt1;
    }
}