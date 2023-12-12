namespace Farmacia.VentasRecetaElectronica
{
    partial class FrmConfigurar_RecetaElectronica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigurar_RecetaElectronica));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkServicioActivo = new System.Windows.Forms.CheckBox();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.scTextBoxExt2 = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt3 = new SC_ControlsCS.scLabelExt();
            this.scTextBoxExt1 = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.txtFolioRecetaElectronica = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.scTextBoxExt3 = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt5 = new SC_ControlsCS.scLabelExt();
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
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(703, 25);
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
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scTextBoxExt3);
            this.groupBox1.Controls.Add(this.scLabelExt5);
            this.groupBox1.Controls.Add(this.chkServicioActivo);
            this.groupBox1.Controls.Add(this.scLabelExt4);
            this.groupBox1.Controls.Add(this.scTextBoxExt2);
            this.groupBox1.Controls.Add(this.scLabelExt3);
            this.groupBox1.Controls.Add(this.scTextBoxExt1);
            this.groupBox1.Controls.Add(this.scLabelExt2);
            this.groupBox1.Controls.Add(this.txtFolioRecetaElectronica);
            this.groupBox1.Controls.Add(this.scLabelExt1);
            this.groupBox1.Location = new System.Drawing.Point(10, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 191);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de configuración";
            // 
            // chkServicioActivo
            // 
            this.chkServicioActivo.Location = new System.Drawing.Point(142, 160);
            this.chkServicioActivo.Name = "chkServicioActivo";
            this.chkServicioActivo.Size = new System.Drawing.Size(104, 20);
            this.chkServicioActivo.TabIndex = 4;
            this.chkServicioActivo.UseVisualStyleBackColor = true;
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Location = new System.Drawing.Point(7, 160);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(137, 18);
            this.scLabelExt4.TabIndex = 9;
            this.scLabelExt4.Text = "Servicio activo : ";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scTextBoxExt2
            // 
            this.scTextBoxExt2.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.scTextBoxExt2.Decimales = 2;
            this.scTextBoxExt2.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.scTextBoxExt2.ForeColor = System.Drawing.Color.Black;
            this.scTextBoxExt2.Location = new System.Drawing.Point(142, 133);
            this.scTextBoxExt2.MaxLength = 150;
            this.scTextBoxExt2.Name = "scTextBoxExt2";
            this.scTextBoxExt2.PermitirApostrofo = false;
            this.scTextBoxExt2.PermitirNegativos = false;
            this.scTextBoxExt2.Size = new System.Drawing.Size(522, 20);
            this.scTextBoxExt2.TabIndex = 3;
            this.scTextBoxExt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelExt3
            // 
            this.scLabelExt3.Location = new System.Drawing.Point(7, 134);
            this.scLabelExt3.MostrarToolTip = false;
            this.scLabelExt3.Name = "scLabelExt3";
            this.scLabelExt3.Size = new System.Drawing.Size(137, 18);
            this.scLabelExt3.TabIndex = 7;
            this.scLabelExt3.Text = "Dirección de repositorio : ";
            this.scLabelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scTextBoxExt1
            // 
            this.scTextBoxExt1.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.scTextBoxExt1.Decimales = 2;
            this.scTextBoxExt1.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.scTextBoxExt1.ForeColor = System.Drawing.Color.Black;
            this.scTextBoxExt1.Location = new System.Drawing.Point(142, 71);
            this.scTextBoxExt1.MaxLength = 150;
            this.scTextBoxExt1.Multiline = true;
            this.scTextBoxExt1.Name = "scTextBoxExt1";
            this.scTextBoxExt1.PermitirApostrofo = false;
            this.scTextBoxExt1.PermitirNegativos = false;
            this.scTextBoxExt1.Size = new System.Drawing.Size(522, 56);
            this.scTextBoxExt1.TabIndex = 2;
            this.scTextBoxExt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(7, 72);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(137, 18);
            this.scLabelExt2.TabIndex = 5;
            this.scLabelExt2.Text = "Nombre unidad médica : ";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioRecetaElectronica
            // 
            this.txtFolioRecetaElectronica.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioRecetaElectronica.Decimales = 2;
            this.txtFolioRecetaElectronica.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioRecetaElectronica.ForeColor = System.Drawing.Color.Black;
            this.txtFolioRecetaElectronica.Location = new System.Drawing.Point(142, 45);
            this.txtFolioRecetaElectronica.MaxLength = 20;
            this.txtFolioRecetaElectronica.Name = "txtFolioRecetaElectronica";
            this.txtFolioRecetaElectronica.PermitirApostrofo = false;
            this.txtFolioRecetaElectronica.PermitirNegativos = false;
            this.txtFolioRecetaElectronica.Size = new System.Drawing.Size(136, 20);
            this.txtFolioRecetaElectronica.TabIndex = 1;
            this.txtFolioRecetaElectronica.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(7, 46);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(137, 18);
            this.scLabelExt1.TabIndex = 3;
            this.scLabelExt1.Text = "CLUES : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scTextBoxExt3
            // 
            this.scTextBoxExt3.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.scTextBoxExt3.Decimales = 2;
            this.scTextBoxExt3.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.scTextBoxExt3.ForeColor = System.Drawing.Color.Black;
            this.scTextBoxExt3.Location = new System.Drawing.Point(142, 19);
            this.scTextBoxExt3.MaxLength = 6;
            this.scTextBoxExt3.Name = "scTextBoxExt3";
            this.scTextBoxExt3.PermitirApostrofo = false;
            this.scTextBoxExt3.PermitirNegativos = false;
            this.scTextBoxExt3.Size = new System.Drawing.Size(136, 20);
            this.scTextBoxExt3.TabIndex = 0;
            this.scTextBoxExt3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelExt5
            // 
            this.scLabelExt5.Location = new System.Drawing.Point(7, 20);
            this.scLabelExt5.MostrarToolTip = false;
            this.scLabelExt5.Name = "scLabelExt5";
            this.scLabelExt5.Size = new System.Drawing.Size(137, 18);
            this.scLabelExt5.TabIndex = 11;
            this.scLabelExt5.Text = "Id Unidad Médica : ";
            this.scLabelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmConfigurar_RecetaElectronica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 227);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmConfigurar_RecetaElectronica";
            this.Text = "Configuración para atención de recetas electrónicas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConfigurar_RecetaElectronica_Load);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtFolioRecetaElectronica;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scTextBoxExt scTextBoxExt1;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private SC_ControlsCS.scTextBoxExt scTextBoxExt2;
        private SC_ControlsCS.scLabelExt scLabelExt3;
        private SC_ControlsCS.scLabelExt scLabelExt4;
        private System.Windows.Forms.CheckBox chkServicioActivo;
        private SC_ControlsCS.scTextBoxExt scTextBoxExt3;
        private SC_ControlsCS.scLabelExt scLabelExt5;
    }
}