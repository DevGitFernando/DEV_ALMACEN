namespace DllFarmaciaSoft.QRCode.GenerarEtiquetas
{
    partial class FrmET_Ubicaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmET_Ubicaciones));
            this.chkMostrarImpresionEnPantalla = new System.Windows.Forms.CheckBox();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReader = new System.Windows.Forms.ToolStripButton();
            this.FrameDatosUbicaciones = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboFormatosEtiquetas = new SC_ControlsCS.scComboBoxExt();
            this.lblEntrepaño = new SC_ControlsCS.scLabelExt();
            this.lblEstante = new SC_ControlsCS.scLabelExt();
            this.lblPasillo = new SC_ControlsCS.scLabelExt();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEntrepaño = new SC_ControlsCS.scTextBoxExt();
            this.txtEstante = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPasillo = new SC_ControlsCS.scTextBoxExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatosUbicaciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkMostrarImpresionEnPantalla
            // 
            this.chkMostrarImpresionEnPantalla.Location = new System.Drawing.Point(398, 19);
            this.chkMostrarImpresionEnPantalla.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMostrarImpresionEnPantalla.Name = "chkMostrarImpresionEnPantalla";
            this.chkMostrarImpresionEnPantalla.Size = new System.Drawing.Size(155, 21);
            this.chkMostrarImpresionEnPantalla.TabIndex = 50;
            this.chkMostrarImpresionEnPantalla.Text = "Mostrar Vista Previa";
            this.chkMostrarImpresionEnPantalla.UseVisualStyleBackColor = true;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator3,
            this.btnImprimir,
            this.toolStripSeparator4,
            this.btnReader});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(597, 58);
            this.toolStripBarraMenu.TabIndex = 49;
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
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(54, 55);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(12, 4);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(12, 4);
            // 
            // btnReader
            // 
            this.btnReader.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReader.Image = ((System.Drawing.Image)(resources.GetObject("btnReader.Image")));
            this.btnReader.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReader.Name = "btnReader";
            this.btnReader.Size = new System.Drawing.Size(54, 55);
            this.btnReader.Text = "Activar lector";
            this.btnReader.Click += new System.EventHandler(this.btnReader_Click);
            // 
            // FrameDatosUbicaciones
            // 
            this.FrameDatosUbicaciones.Controls.Add(this.label1);
            this.FrameDatosUbicaciones.Controls.Add(this.chkMostrarImpresionEnPantalla);
            this.FrameDatosUbicaciones.Controls.Add(this.cboFormatosEtiquetas);
            this.FrameDatosUbicaciones.Controls.Add(this.lblEntrepaño);
            this.FrameDatosUbicaciones.Controls.Add(this.lblEstante);
            this.FrameDatosUbicaciones.Controls.Add(this.lblPasillo);
            this.FrameDatosUbicaciones.Controls.Add(this.label12);
            this.FrameDatosUbicaciones.Controls.Add(this.label11);
            this.FrameDatosUbicaciones.Controls.Add(this.txtEntrepaño);
            this.FrameDatosUbicaciones.Controls.Add(this.txtEstante);
            this.FrameDatosUbicaciones.Controls.Add(this.label10);
            this.FrameDatosUbicaciones.Controls.Add(this.txtPasillo);
            this.FrameDatosUbicaciones.Location = new System.Drawing.Point(11, 68);
            this.FrameDatosUbicaciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatosUbicaciones.Name = "FrameDatosUbicaciones";
            this.FrameDatosUbicaciones.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatosUbicaciones.Size = new System.Drawing.Size(571, 194);
            this.FrameDatosUbicaciones.TabIndex = 51;
            this.FrameDatosUbicaciones.TabStop = false;
            this.FrameDatosUbicaciones.Text = "Ubicación";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 148);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 25);
            this.label1.TabIndex = 58;
            this.label1.Text = "Etiqueta :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFormatosEtiquetas
            // 
            this.cboFormatosEtiquetas.BackColorEnabled = System.Drawing.Color.White;
            this.cboFormatosEtiquetas.Data = "";
            this.cboFormatosEtiquetas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFormatosEtiquetas.Filtro = " 1 = 1";
            this.cboFormatosEtiquetas.FormattingEnabled = true;
            this.cboFormatosEtiquetas.ListaItemsBusqueda = 20;
            this.cboFormatosEtiquetas.Location = new System.Drawing.Point(123, 147);
            this.cboFormatosEtiquetas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboFormatosEtiquetas.MostrarToolTip = false;
            this.cboFormatosEtiquetas.Name = "cboFormatosEtiquetas";
            this.cboFormatosEtiquetas.Size = new System.Drawing.Size(417, 24);
            this.cboFormatosEtiquetas.TabIndex = 57;
            // 
            // lblEntrepaño
            // 
            this.lblEntrepaño.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEntrepaño.Location = new System.Drawing.Point(223, 115);
            this.lblEntrepaño.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntrepaño.MostrarToolTip = false;
            this.lblEntrepaño.Name = "lblEntrepaño";
            this.lblEntrepaño.Size = new System.Drawing.Size(319, 25);
            this.lblEntrepaño.TabIndex = 56;
            this.lblEntrepaño.Text = "scLabelExt2";
            this.lblEntrepaño.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEstante
            // 
            this.lblEstante.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstante.Location = new System.Drawing.Point(223, 81);
            this.lblEstante.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstante.MostrarToolTip = false;
            this.lblEstante.Name = "lblEstante";
            this.lblEstante.Size = new System.Drawing.Size(319, 25);
            this.lblEstante.TabIndex = 55;
            this.lblEstante.Text = "scLabelExt1";
            this.lblEstante.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPasillo
            // 
            this.lblPasillo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPasillo.Location = new System.Drawing.Point(223, 49);
            this.lblPasillo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPasillo.MostrarToolTip = false;
            this.lblPasillo.Name = "lblPasillo";
            this.lblPasillo.Size = new System.Drawing.Size(319, 25);
            this.lblPasillo.TabIndex = 54;
            this.lblPasillo.Text = "scLabelExt1";
            this.lblPasillo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(29, 115);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 25);
            this.label12.TabIndex = 53;
            this.label12.Text = "Posición :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(29, 81);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 25);
            this.label11.TabIndex = 52;
            this.label11.Text = "Nivel :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEntrepaño
            // 
            this.txtEntrepaño.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEntrepaño.Decimales = 2;
            this.txtEntrepaño.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEntrepaño.ForeColor = System.Drawing.Color.Black;
            this.txtEntrepaño.Location = new System.Drawing.Point(123, 115);
            this.txtEntrepaño.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEntrepaño.MaxLength = 8;
            this.txtEntrepaño.Name = "txtEntrepaño";
            this.txtEntrepaño.PermitirApostrofo = false;
            this.txtEntrepaño.PermitirNegativos = false;
            this.txtEntrepaño.Size = new System.Drawing.Size(93, 22);
            this.txtEntrepaño.TabIndex = 51;
            this.txtEntrepaño.Text = "01234567";
            this.txtEntrepaño.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEntrepaño.Validating += new System.ComponentModel.CancelEventHandler(this.txtEntrepaño_Validating);
            // 
            // txtEstante
            // 
            this.txtEstante.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEstante.Decimales = 2;
            this.txtEstante.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEstante.ForeColor = System.Drawing.Color.Black;
            this.txtEstante.Location = new System.Drawing.Point(123, 81);
            this.txtEstante.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEstante.MaxLength = 8;
            this.txtEstante.Name = "txtEstante";
            this.txtEstante.PermitirApostrofo = false;
            this.txtEstante.PermitirNegativos = false;
            this.txtEstante.Size = new System.Drawing.Size(93, 22);
            this.txtEstante.TabIndex = 50;
            this.txtEstante.Text = "01234567";
            this.txtEstante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEstante.Validating += new System.ComponentModel.CancelEventHandler(this.txtEstante_Validating);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(29, 49);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 25);
            this.label10.TabIndex = 49;
            this.label10.Text = "Rack :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPasillo
            // 
            this.txtPasillo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPasillo.Decimales = 2;
            this.txtPasillo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPasillo.ForeColor = System.Drawing.Color.Black;
            this.txtPasillo.Location = new System.Drawing.Point(123, 49);
            this.txtPasillo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPasillo.MaxLength = 8;
            this.txtPasillo.Name = "txtPasillo";
            this.txtPasillo.PermitirApostrofo = false;
            this.txtPasillo.PermitirNegativos = false;
            this.txtPasillo.Size = new System.Drawing.Size(93, 22);
            this.txtPasillo.TabIndex = 0;
            this.txtPasillo.Text = "01234567";
            this.txtPasillo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPasillo.Validating += new System.ComponentModel.CancelEventHandler(this.txtPasillo_Validating);
            // 
            // FrmET_Ubicaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 275);
            this.Controls.Add(this.FrameDatosUbicaciones);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmET_Ubicaciones";
            this.ShowIcon = false;
            this.Text = "Etiquetas para Racks (Ubicaciones)";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatosUbicaciones.ResumeLayout(false);
            this.FrameDatosUbicaciones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMostrarImpresionEnPantalla;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnReader;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameDatosUbicaciones;
        private SC_ControlsCS.scTextBoxExt txtPasillo;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scTextBoxExt txtEntrepaño;
        private SC_ControlsCS.scTextBoxExt txtEstante;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private SC_ControlsCS.scLabelExt lblEntrepaño;
        private SC_ControlsCS.scLabelExt lblEstante;
        private SC_ControlsCS.scLabelExt lblPasillo;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboFormatosEtiquetas;
    }
}