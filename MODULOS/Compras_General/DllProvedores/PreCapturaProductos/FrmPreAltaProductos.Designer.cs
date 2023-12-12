namespace DllProveedores.PreCapturaProductos
{
    partial class FrmPreAltaProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPreAltaProductos));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabProductos = new System.Windows.Forms.TabPage();
            this.FrameProducto = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkRefrigerado = new System.Windows.Forms.CheckBox();
            this.txtCodigoEAN = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtLaboratorio = new SC_ControlsCS.scTextBoxExt();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.txtIdClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPrecioMaximo = new SC_ControlsCS.scNumericTextBox();
            this.chkEsSectorSalud = new System.Windows.Forms.CheckBox();
            this.chkMedicamento = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboPresentaciones = new SC_ControlsCS.scComboBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTipoProductos = new SC_ControlsCS.scComboBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.cboClasificaciones = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProducto = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.tabCatProductos = new System.Windows.Forms.TabControl();
            this.toolStripBarraMenu.SuspendLayout();
            this.tabProductos.SuspendLayout();
            this.FrameProducto.SuspendLayout();
            this.tabCatProductos.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(538, 25);
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
            this.btnNuevo.ToolTipText = "[F4] Nuevo";
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
            this.btnGuardar.ToolTipText = "[F6] Guardar";
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
            this.btnCancelar.ToolTipText = "[F8] Cancelar";
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
            this.btnImprimir.ToolTipText = "[F10] Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // tabProductos
            // 
            this.tabProductos.Controls.Add(this.FrameProducto);
            this.tabProductos.Location = new System.Drawing.Point(4, 22);
            this.tabProductos.Name = "tabProductos";
            this.tabProductos.Padding = new System.Windows.Forms.Padding(3);
            this.tabProductos.Size = new System.Drawing.Size(512, 335);
            this.tabProductos.TabIndex = 0;
            this.tabProductos.Text = "Datos de Producto";
            this.tabProductos.UseVisualStyleBackColor = true;
            // 
            // FrameProducto
            // 
            this.FrameProducto.Controls.Add(this.label4);
            this.FrameProducto.Controls.Add(this.chkRefrigerado);
            this.FrameProducto.Controls.Add(this.txtCodigoEAN);
            this.FrameProducto.Controls.Add(this.label1);
            this.FrameProducto.Controls.Add(this.lblCancelado);
            this.FrameProducto.Controls.Add(this.txtLaboratorio);
            this.FrameProducto.Controls.Add(this.txtClaveSSA);
            this.FrameProducto.Controls.Add(this.txtIdClaveSSA);
            this.FrameProducto.Controls.Add(this.label16);
            this.FrameProducto.Controls.Add(this.txtPrecioMaximo);
            this.FrameProducto.Controls.Add(this.chkEsSectorSalud);
            this.FrameProducto.Controls.Add(this.chkMedicamento);
            this.FrameProducto.Controls.Add(this.label10);
            this.FrameProducto.Controls.Add(this.cboPresentaciones);
            this.FrameProducto.Controls.Add(this.label9);
            this.FrameProducto.Controls.Add(this.label6);
            this.FrameProducto.Controls.Add(this.cboTipoProductos);
            this.FrameProducto.Controls.Add(this.label5);
            this.FrameProducto.Controls.Add(this.cboClasificaciones);
            this.FrameProducto.Controls.Add(this.label3);
            this.FrameProducto.Controls.Add(this.txtProducto);
            this.FrameProducto.Controls.Add(this.label2);
            this.FrameProducto.Location = new System.Drawing.Point(6, 6);
            this.FrameProducto.Name = "FrameProducto";
            this.FrameProducto.Size = new System.Drawing.Size(497, 317);
            this.FrameProducto.TabIndex = 0;
            this.FrameProducto.TabStop = false;
            this.FrameProducto.Text = "Datos Producto :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(14, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 43;
            this.label4.Text = "Descripcion SSA :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkRefrigerado
            // 
            this.chkRefrigerado.AutoSize = true;
            this.chkRefrigerado.Location = new System.Drawing.Point(114, 285);
            this.chkRefrigerado.Name = "chkRefrigerado";
            this.chkRefrigerado.Size = new System.Drawing.Size(96, 17);
            this.chkRefrigerado.TabIndex = 11;
            this.chkRefrigerado.Text = "Es Refrigerado";
            this.chkRefrigerado.UseVisualStyleBackColor = true;
            // 
            // txtCodigoEAN
            // 
            this.txtCodigoEAN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoEAN.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoEAN.Decimales = 2;
            this.txtCodigoEAN.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoEAN.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoEAN.Location = new System.Drawing.Point(114, 21);
            this.txtCodigoEAN.MaxLength = 30;
            this.txtCodigoEAN.Name = "txtCodigoEAN";
            this.txtCodigoEAN.PermitirApostrofo = false;
            this.txtCodigoEAN.PermitirNegativos = false;
            this.txtCodigoEAN.Size = new System.Drawing.Size(173, 20);
            this.txtCodigoEAN.TabIndex = 0;
            this.txtCodigoEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoEAN_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 41;
            this.label1.Text = "Codigo EAN :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(293, 21);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(87, 20);
            this.lblCancelado.TabIndex = 39;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtLaboratorio
            // 
            this.txtLaboratorio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLaboratorio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtLaboratorio.Decimales = 2;
            this.txtLaboratorio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtLaboratorio.ForeColor = System.Drawing.Color.Black;
            this.txtLaboratorio.Location = new System.Drawing.Point(114, 127);
            this.txtLaboratorio.MaxLength = 200;
            this.txtLaboratorio.Name = "txtLaboratorio";
            this.txtLaboratorio.PermitirApostrofo = false;
            this.txtLaboratorio.PermitirNegativos = false;
            this.txtLaboratorio.Size = new System.Drawing.Size(375, 20);
            this.txtLaboratorio.TabIndex = 4;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(114, 75);
            this.txtClaveSSA.MaxLength = 100;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(375, 20);
            this.txtClaveSSA.TabIndex = 2;
            // 
            // txtIdClaveSSA
            // 
            this.txtIdClaveSSA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdClaveSSA.Decimales = 2;
            this.txtIdClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtIdClaveSSA.Location = new System.Drawing.Point(114, 47);
            this.txtIdClaveSSA.MaxLength = 15;
            this.txtIdClaveSSA.Name = "txtIdClaveSSA";
            this.txtIdClaveSSA.PermitirApostrofo = false;
            this.txtIdClaveSSA.PermitirNegativos = false;
            this.txtIdClaveSSA.Size = new System.Drawing.Size(173, 20);
            this.txtIdClaveSSA.TabIndex = 1;
            this.txtIdClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdClaveSSA_Validating);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(14, 233);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 36);
            this.label16.TabIndex = 35;
            this.label16.Text = "Precio Máximo Publico:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrecioMaximo
            // 
            this.txtPrecioMaximo.AllowNegative = true;
            this.txtPrecioMaximo.DigitsInGroup = 3;
            this.txtPrecioMaximo.Flags = 7680;
            this.txtPrecioMaximo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioMaximo.Location = new System.Drawing.Point(116, 234);
            this.txtPrecioMaximo.MaxDecimalPlaces = 4;
            this.txtPrecioMaximo.MaxLength = 15;
            this.txtPrecioMaximo.MaxWholeDigits = 15;
            this.txtPrecioMaximo.Name = "txtPrecioMaximo";
            this.txtPrecioMaximo.Prefix = "";
            this.txtPrecioMaximo.RangeMax = 1.7976931348623157E+308;
            this.txtPrecioMaximo.RangeMin = -1.7976931348623157E+308;
            this.txtPrecioMaximo.Size = new System.Drawing.Size(92, 22);
            this.txtPrecioMaximo.TabIndex = 8;
            this.txtPrecioMaximo.Text = "0.0000";
            this.txtPrecioMaximo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkEsSectorSalud
            // 
            this.chkEsSectorSalud.AutoSize = true;
            this.chkEsSectorSalud.Location = new System.Drawing.Point(314, 262);
            this.chkEsSectorSalud.Name = "chkEsSectorSalud";
            this.chkEsSectorSalud.Size = new System.Drawing.Size(119, 17);
            this.chkEsSectorSalud.TabIndex = 10;
            this.chkEsSectorSalud.Text = "Es del Sector Salud";
            this.chkEsSectorSalud.UseVisualStyleBackColor = true;
            // 
            // chkMedicamento
            // 
            this.chkMedicamento.AutoSize = true;
            this.chkMedicamento.Location = new System.Drawing.Point(114, 262);
            this.chkMedicamento.Name = "chkMedicamento";
            this.chkMedicamento.Size = new System.Drawing.Size(159, 17);
            this.chkMedicamento.TabIndex = 9;
            this.chkMedicamento.Text = "Es Medicamento Controlado";
            this.chkMedicamento.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(11, 211);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Presentación :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPresentaciones
            // 
            this.cboPresentaciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboPresentaciones.Data = "";
            this.cboPresentaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPresentaciones.FormattingEnabled = true;
            this.cboPresentaciones.Location = new System.Drawing.Point(116, 207);
            this.cboPresentaciones.Name = "cboPresentaciones";
            this.cboPresentaciones.Size = new System.Drawing.Size(373, 21);
            this.cboPresentaciones.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(19, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Laboratorio :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Tipo de producto :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoProductos
            // 
            this.cboTipoProductos.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoProductos.Data = "";
            this.cboTipoProductos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoProductos.FormattingEnabled = true;
            this.cboTipoProductos.Location = new System.Drawing.Point(116, 153);
            this.cboTipoProductos.Name = "cboTipoProductos";
            this.cboTipoProductos.Size = new System.Drawing.Size(375, 21);
            this.cboTipoProductos.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(36, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Clasificación :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboClasificaciones
            // 
            this.cboClasificaciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboClasificaciones.Data = "";
            this.cboClasificaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClasificaciones.FormattingEnabled = true;
            this.cboClasificaciones.Location = new System.Drawing.Point(116, 180);
            this.cboClasificaciones.Name = "cboClasificaciones";
            this.cboClasificaciones.Size = new System.Drawing.Size(373, 21);
            this.cboClasificaciones.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(44, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Clave SSA :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProducto
            // 
            this.txtProducto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProducto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtProducto.Decimales = 2;
            this.txtProducto.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtProducto.ForeColor = System.Drawing.Color.Black;
            this.txtProducto.Location = new System.Drawing.Point(114, 101);
            this.txtProducto.MaxLength = 200;
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.PermitirApostrofo = false;
            this.txtProducto.PermitirNegativos = false;
            this.txtProducto.Size = new System.Drawing.Size(375, 20);
            this.txtProducto.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(39, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Producto:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabCatProductos
            // 
            this.tabCatProductos.Controls.Add(this.tabProductos);
            this.tabCatProductos.Location = new System.Drawing.Point(11, 27);
            this.tabCatProductos.Name = "tabCatProductos";
            this.tabCatProductos.SelectedIndex = 0;
            this.tabCatProductos.Size = new System.Drawing.Size(520, 361);
            this.tabCatProductos.TabIndex = 0;
            // 
            // FrmPreAltaProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 398);
            this.Controls.Add(this.tabCatProductos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmPreAltaProductos";
            this.Text = "Productos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPreAltaProductos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.tabProductos.ResumeLayout(false);
            this.FrameProducto.ResumeLayout(false);
            this.FrameProducto.PerformLayout();
            this.tabCatProductos.ResumeLayout(false);
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
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tabProductos;
        private System.Windows.Forms.GroupBox FrameProducto;
        private System.Windows.Forms.Label label16;
        private SC_ControlsCS.scNumericTextBox txtPrecioMaximo;
        private System.Windows.Forms.CheckBox chkEsSectorSalud;
        private System.Windows.Forms.CheckBox chkMedicamento;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scComboBoxExt cboPresentaciones;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboTipoProductos;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scComboBoxExt cboClasificaciones;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtProducto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabCatProductos;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private SC_ControlsCS.scTextBoxExt txtIdClaveSSA;
        private SC_ControlsCS.scTextBoxExt txtLaboratorio;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtCodigoEAN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRefrigerado;
        private System.Windows.Forms.Label label4;
    }
}