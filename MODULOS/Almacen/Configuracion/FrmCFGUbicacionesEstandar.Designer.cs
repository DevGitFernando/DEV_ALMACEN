namespace Almacen.Configuracion
{
    partial class FrmCFGUbicacionesEstandar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCFGUbicacionesEstandar));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.lblEntrepaño = new System.Windows.Forms.Label();
            this.txtEntrepaño = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.lblNivel = new System.Windows.Forms.Label();
            this.txtNivel = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRack = new System.Windows.Forms.Label();
            this.txtRack = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPosicion = new SC_ControlsCS.scLabelExt();
            this.cboPosicion = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator4,
            this.btnCancelar,
            this.toolStripSeparator3,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(680, 58);
            this.toolStripBarraMenu.TabIndex = 6;
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
            this.toolStripSeparator.Size = new System.Drawing.Size(10, 58);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(54, 55);
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(10, 58);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(54, 55);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(10, 58);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(54, 55);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkStatus);
            this.groupBox1.Controls.Add(this.lblEntrepaño);
            this.groupBox1.Controls.Add(this.txtEntrepaño);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblNivel);
            this.groupBox1.Controls.Add(this.txtNivel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblRack);
            this.groupBox1.Controls.Add(this.txtRack);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblPosicion);
            this.groupBox1.Controls.Add(this.cboPosicion);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(15, 61);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(648, 279);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Enabled = false;
            this.chkStatus.Location = new System.Drawing.Point(528, 23);
            this.chkStatus.Margin = new System.Windows.Forms.Padding(4);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(91, 20);
            this.chkStatus.TabIndex = 66;
            this.chkStatus.Text = "Habilitado";
            this.chkStatus.UseVisualStyleBackColor = true;
            this.chkStatus.Visible = false;
            // 
            // lblEntrepaño
            // 
            this.lblEntrepaño.Location = new System.Drawing.Point(211, 234);
            this.lblEntrepaño.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntrepaño.Name = "lblEntrepaño";
            this.lblEntrepaño.Size = new System.Drawing.Size(405, 25);
            this.lblEntrepaño.TabIndex = 65;
            this.lblEntrepaño.Text = "label1";
            // 
            // txtEntrepaño
            // 
            this.txtEntrepaño.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEntrepaño.Decimales = 2;
            this.txtEntrepaño.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEntrepaño.ForeColor = System.Drawing.Color.Black;
            this.txtEntrepaño.Location = new System.Drawing.Point(124, 234);
            this.txtEntrepaño.Margin = new System.Windows.Forms.Padding(4);
            this.txtEntrepaño.MaxLength = 4;
            this.txtEntrepaño.Name = "txtEntrepaño";
            this.txtEntrepaño.PermitirApostrofo = false;
            this.txtEntrepaño.PermitirNegativos = false;
            this.txtEntrepaño.Size = new System.Drawing.Size(77, 22);
            this.txtEntrepaño.TabIndex = 59;
            this.txtEntrepaño.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEntrepaño.Validating += new System.ComponentModel.CancelEventHandler(this.txtEntrepaño_Validating);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(24, 236);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 20);
            this.label10.TabIndex = 64;
            this.label10.Text = "Posición :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNivel
            // 
            this.lblNivel.Location = new System.Drawing.Point(211, 203);
            this.lblNivel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNivel.Name = "lblNivel";
            this.lblNivel.Size = new System.Drawing.Size(405, 25);
            this.lblNivel.TabIndex = 63;
            this.lblNivel.Text = "label1";
            // 
            // txtNivel
            // 
            this.txtNivel.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNivel.Decimales = 2;
            this.txtNivel.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtNivel.ForeColor = System.Drawing.Color.Black;
            this.txtNivel.Location = new System.Drawing.Point(124, 203);
            this.txtNivel.Margin = new System.Windows.Forms.Padding(4);
            this.txtNivel.MaxLength = 4;
            this.txtNivel.Name = "txtNivel";
            this.txtNivel.PermitirApostrofo = false;
            this.txtNivel.PermitirNegativos = false;
            this.txtNivel.Size = new System.Drawing.Size(77, 22);
            this.txtNivel.TabIndex = 58;
            this.txtNivel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNivel.Validating += new System.ComponentModel.CancelEventHandler(this.txtNivel_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(55, 206);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 62;
            this.label3.Text = "Nivel :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRack
            // 
            this.lblRack.Location = new System.Drawing.Point(211, 172);
            this.lblRack.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRack.Name = "lblRack";
            this.lblRack.Size = new System.Drawing.Size(405, 25);
            this.lblRack.TabIndex = 61;
            this.lblRack.Text = "label1";
            // 
            // txtRack
            // 
            this.txtRack.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRack.Decimales = 2;
            this.txtRack.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtRack.ForeColor = System.Drawing.Color.Black;
            this.txtRack.Location = new System.Drawing.Point(124, 172);
            this.txtRack.Margin = new System.Windows.Forms.Padding(4);
            this.txtRack.MaxLength = 4;
            this.txtRack.Name = "txtRack";
            this.txtRack.PermitirApostrofo = false;
            this.txtRack.PermitirNegativos = false;
            this.txtRack.Size = new System.Drawing.Size(77, 22);
            this.txtRack.TabIndex = 57;
            this.txtRack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRack.Validating += new System.ComponentModel.CancelEventHandler(this.txtRack_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(55, 175);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 60;
            this.label1.Text = "Rack :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPosicion
            // 
            this.lblPosicion.Location = new System.Drawing.Point(124, 90);
            this.lblPosicion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPosicion.MostrarToolTip = false;
            this.lblPosicion.Name = "lblPosicion";
            this.lblPosicion.Size = new System.Drawing.Size(492, 71);
            this.lblPosicion.TabIndex = 40;
            this.lblPosicion.Text = "scLabelExt1";
            // 
            // cboPosicion
            // 
            this.cboPosicion.BackColorEnabled = System.Drawing.Color.White;
            this.cboPosicion.Data = "";
            this.cboPosicion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPosicion.Enabled = false;
            this.cboPosicion.Filtro = " 1 = 1";
            this.cboPosicion.FormattingEnabled = true;
            this.cboPosicion.ListaItemsBusqueda = 20;
            this.cboPosicion.Location = new System.Drawing.Point(124, 53);
            this.cboPosicion.Margin = new System.Windows.Forms.Padding(4);
            this.cboPosicion.MostrarToolTip = false;
            this.cboPosicion.Name = "cboPosicion";
            this.cboPosicion.Size = new System.Drawing.Size(491, 24);
            this.cboPosicion.TabIndex = 39;
            this.cboPosicion.SelectedIndexChanged += new System.EventHandler(this.cboPosicion_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(36, 57);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 17);
            this.label6.TabIndex = 38;
            this.label6.Text = "Area :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmCFGUbicacionesEstandar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 356);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmCFGUbicacionesEstandar";
            this.ShowIcon = false;
            this.Text = "Configuración Ubicaciones Estandar";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCFGUbicacionesEstandar_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scComboBoxExt cboPosicion;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scLabelExt lblPosicion;
        private System.Windows.Forms.Label lblEntrepaño;
        private SC_ControlsCS.scTextBoxExt txtEntrepaño;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblNivel;
        private SC_ControlsCS.scTextBoxExt txtNivel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRack;
        private SC_ControlsCS.scTextBoxExt txtRack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}