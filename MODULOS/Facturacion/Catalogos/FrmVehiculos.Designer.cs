namespace Facturacion.Catalogos
{
    partial class FrmVehiculos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVehiculos));
            this.FramePrincipal = new System.Windows.Forms.GroupBox();
            this.cboClaveVehiculo = new SC_ControlsCS.scComboBoxExt();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.cboPermSCT = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPlacas = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumSerie = new SC_ControlsCS.scTextBoxExt();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpModelo = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMarca = new SC_ControlsCS.scTextBoxExt();
            this.txtAseguradora = new SC_ControlsCS.scTextBoxExt();
            this.label18 = new System.Windows.Forms.Label();
            this.txtNumPermSCT = new SC_ControlsCS.scTextBoxExt();
            this.label17 = new System.Windows.Forms.Label();
            this.PermSCT = new System.Windows.Forms.Label();
            this.txtVehiculo = new SC_ControlsCS.scTextBoxExt();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAlias = new SC_ControlsCS.scTextBoxExt();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNumPoliza = new SC_ControlsCS.scTextBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FramePrincipal.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FramePrincipal
            // 
            this.FramePrincipal.Controls.Add(this.cboClaveVehiculo);
            this.FramePrincipal.Controls.Add(this.lblCancelado);
            this.FramePrincipal.Controls.Add(this.cboPermSCT);
            this.FramePrincipal.Controls.Add(this.label8);
            this.FramePrincipal.Controls.Add(this.txtPlacas);
            this.FramePrincipal.Controls.Add(this.label1);
            this.FramePrincipal.Controls.Add(this.txtNumSerie);
            this.FramePrincipal.Controls.Add(this.label12);
            this.FramePrincipal.Controls.Add(this.dtpModelo);
            this.FramePrincipal.Controls.Add(this.label16);
            this.FramePrincipal.Controls.Add(this.txtMarca);
            this.FramePrincipal.Controls.Add(this.txtAseguradora);
            this.FramePrincipal.Controls.Add(this.label18);
            this.FramePrincipal.Controls.Add(this.txtNumPermSCT);
            this.FramePrincipal.Controls.Add(this.label17);
            this.FramePrincipal.Controls.Add(this.PermSCT);
            this.FramePrincipal.Controls.Add(this.txtVehiculo);
            this.FramePrincipal.Controls.Add(this.label13);
            this.FramePrincipal.Controls.Add(this.label2);
            this.FramePrincipal.Controls.Add(this.label3);
            this.FramePrincipal.Controls.Add(this.txtAlias);
            this.FramePrincipal.Controls.Add(this.label11);
            this.FramePrincipal.Controls.Add(this.txtNumPoliza);
            this.FramePrincipal.Location = new System.Drawing.Point(12, 28);
            this.FramePrincipal.Name = "FramePrincipal";
            this.FramePrincipal.Size = new System.Drawing.Size(625, 277);
            this.FramePrincipal.TabIndex = 0;
            this.FramePrincipal.TabStop = false;
            this.FramePrincipal.Text = "Datos Generales del Vehiculo";
            // 
            // cboClaveVehiculo
            // 
            this.cboClaveVehiculo.BackColorEnabled = System.Drawing.Color.White;
            this.cboClaveVehiculo.Data = "";
            this.cboClaveVehiculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClaveVehiculo.Filtro = " 1 = 1";
            this.cboClaveVehiculo.FormattingEnabled = true;
            this.cboClaveVehiculo.ListaItemsBusqueda = 20;
            this.cboClaveVehiculo.Location = new System.Drawing.Point(119, 121);
            this.cboClaveVehiculo.MostrarToolTip = false;
            this.cboClaveVehiculo.Name = "cboClaveVehiculo";
            this.cboClaveVehiculo.Size = new System.Drawing.Size(492, 21);
            this.cboClaveVehiculo.TabIndex = 16;
            // 
            // lblCancelado
            // 
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(234, 22);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(80, 13);
            this.lblCancelado.TabIndex = 105;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.Visible = false;
            // 
            // cboPermSCT
            // 
            this.cboPermSCT.BackColorEnabled = System.Drawing.Color.White;
            this.cboPermSCT.Data = "";
            this.cboPermSCT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPermSCT.Filtro = " 1 = 1";
            this.cboPermSCT.FormattingEnabled = true;
            this.cboPermSCT.ListaItemsBusqueda = 20;
            this.cboPermSCT.Location = new System.Drawing.Point(119, 146);
            this.cboPermSCT.MostrarToolTip = false;
            this.cboPermSCT.Name = "cboPermSCT";
            this.cboPermSCT.Size = new System.Drawing.Size(492, 21);
            this.cboPermSCT.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(26, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 16);
            this.label8.TabIndex = 103;
            this.label8.Text = "Placas :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPlacas
            // 
            this.txtPlacas.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlacas.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPlacas.Decimales = 2;
            this.txtPlacas.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPlacas.ForeColor = System.Drawing.Color.Black;
            this.txtPlacas.Location = new System.Drawing.Point(119, 96);
            this.txtPlacas.MaxLength = 100;
            this.txtPlacas.Name = "txtPlacas";
            this.txtPlacas.PermitirApostrofo = false;
            this.txtPlacas.PermitirNegativos = false;
            this.txtPlacas.Size = new System.Drawing.Size(135, 20);
            this.txtPlacas.TabIndex = 14;
            this.txtPlacas.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(26, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 102;
            this.label1.Text = "Número de serie :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumSerie
            // 
            this.txtNumSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumSerie.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumSerie.Decimales = 2;
            this.txtNumSerie.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumSerie.ForeColor = System.Drawing.Color.Black;
            this.txtNumSerie.Location = new System.Drawing.Point(119, 70);
            this.txtNumSerie.MaxLength = 100;
            this.txtNumSerie.Name = "txtNumSerie";
            this.txtNumSerie.PermitirApostrofo = false;
            this.txtNumSerie.PermitirNegativos = false;
            this.txtNumSerie.Size = new System.Drawing.Size(492, 20);
            this.txtNumSerie.TabIndex = 13;
            this.txtNumSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(467, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 20);
            this.label12.TabIndex = 4;
            this.label12.Text = "Modelo :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpModelo
            // 
            this.dtpModelo.CustomFormat = "yyyy";
            this.dtpModelo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpModelo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpModelo.Location = new System.Drawing.Point(518, 96);
            this.dtpModelo.Name = "dtpModelo";
            this.dtpModelo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpModelo.Size = new System.Drawing.Size(46, 20);
            this.dtpModelo.TabIndex = 15;
            this.dtpModelo.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(44, 46);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(73, 16);
            this.label16.TabIndex = 99;
            this.label16.Text = "Marca :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMarca
            // 
            this.txtMarca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMarca.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtMarca.Decimales = 2;
            this.txtMarca.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtMarca.ForeColor = System.Drawing.Color.Black;
            this.txtMarca.Location = new System.Drawing.Point(119, 44);
            this.txtMarca.MaxLength = 100;
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.PermitirApostrofo = false;
            this.txtMarca.PermitirNegativos = false;
            this.txtMarca.Size = new System.Drawing.Size(492, 20);
            this.txtMarca.TabIndex = 12;
            this.txtMarca.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtAseguradora
            // 
            this.txtAseguradora.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAseguradora.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtAseguradora.Decimales = 2;
            this.txtAseguradora.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtAseguradora.ForeColor = System.Drawing.Color.Black;
            this.txtAseguradora.Location = new System.Drawing.Point(119, 196);
            this.txtAseguradora.MaxLength = 200;
            this.txtAseguradora.Name = "txtAseguradora";
            this.txtAseguradora.PermitirApostrofo = false;
            this.txtAseguradora.PermitirNegativos = false;
            this.txtAseguradora.Size = new System.Drawing.Size(492, 20);
            this.txtAseguradora.TabIndex = 19;
            this.txtAseguradora.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(9, 199);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(110, 13);
            this.label18.TabIndex = 93;
            this.label18.Text = "Aseguradora :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumPermSCT
            // 
            this.txtNumPermSCT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumPermSCT.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumPermSCT.Decimales = 2;
            this.txtNumPermSCT.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumPermSCT.ForeColor = System.Drawing.Color.Black;
            this.txtNumPermSCT.Location = new System.Drawing.Point(119, 170);
            this.txtNumPermSCT.MaxLength = 30;
            this.txtNumPermSCT.Name = "txtNumPermSCT";
            this.txtNumPermSCT.PermitirApostrofo = false;
            this.txtNumPermSCT.PermitirNegativos = false;
            this.txtNumPermSCT.Size = new System.Drawing.Size(492, 20);
            this.txtNumPermSCT.TabIndex = 18;
            this.txtNumPermSCT.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(8, 173);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(110, 13);
            this.label17.TabIndex = 91;
            this.label17.Text = "Número de permiso :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PermSCT
            // 
            this.PermSCT.Location = new System.Drawing.Point(8, 150);
            this.PermSCT.Name = "PermSCT";
            this.PermSCT.Size = new System.Drawing.Size(110, 13);
            this.PermSCT.TabIndex = 89;
            this.PermSCT.Text = "Permiso SCT :";
            this.PermSCT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVehiculo
            // 
            this.txtVehiculo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtVehiculo.Decimales = 2;
            this.txtVehiculo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtVehiculo.ForeColor = System.Drawing.Color.Black;
            this.txtVehiculo.Location = new System.Drawing.Point(119, 19);
            this.txtVehiculo.Name = "txtVehiculo";
            this.txtVehiculo.PermitirApostrofo = false;
            this.txtVehiculo.PermitirNegativos = false;
            this.txtVehiculo.Size = new System.Drawing.Size(100, 20);
            this.txtVehiculo.TabIndex = 11;
            this.txtVehiculo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVehiculo.Validating += new System.ComponentModel.CancelEventHandler(this.txtVehiculo_Validating);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(10, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(110, 13);
            this.label13.TabIndex = 79;
            this.label13.Text = "Clave  :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Alias :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "Clave Vehiculo :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAlias
            // 
            this.txtAlias.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAlias.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtAlias.Decimales = 2;
            this.txtAlias.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtAlias.ForeColor = System.Drawing.Color.Black;
            this.txtAlias.Location = new System.Drawing.Point(119, 246);
            this.txtAlias.MaxLength = 100;
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.PermitirApostrofo = false;
            this.txtAlias.PermitirNegativos = false;
            this.txtAlias.Size = new System.Drawing.Size(492, 20);
            this.txtAlias.TabIndex = 21;
            this.txtAlias.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(10, 226);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 13);
            this.label11.TabIndex = 49;
            this.label11.Text = "Número de póliza :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumPoliza
            // 
            this.txtNumPoliza.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumPoliza.Decimales = 2;
            this.txtNumPoliza.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumPoliza.ForeColor = System.Drawing.Color.Black;
            this.txtNumPoliza.Location = new System.Drawing.Point(119, 222);
            this.txtNumPoliza.Name = "txtNumPoliza";
            this.txtNumPoliza.PermitirApostrofo = false;
            this.txtNumPoliza.PermitirNegativos = false;
            this.txtNumPoliza.Size = new System.Drawing.Size(492, 20);
            this.txtNumPoliza.TabIndex = 20;
            this.txtNumPoliza.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(649, 25);
            this.toolStripBarraMenu.TabIndex = 3;
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
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // FrmVehiculos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 316);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FramePrincipal);
            this.Name = "FrmVehiculos";
            this.Text = "Vehiculos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmVehiculos_Load);
            this.FramePrincipal.ResumeLayout(false);
            this.FramePrincipal.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox FramePrincipal;
        private System.Windows.Forms.Label label11;
        private SC_ControlsCS.scTextBoxExt txtNumPoliza;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtAlias;
        private SC_ControlsCS.scTextBoxExt txtVehiculo;
        private System.Windows.Forms.Label label13;
        private SC_ControlsCS.scTextBoxExt txtAseguradora;
        private System.Windows.Forms.Label label18;
        private SC_ControlsCS.scTextBoxExt txtNumPermSCT;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label PermSCT;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scTextBoxExt txtPlacas;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtNumSerie;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpModelo;
        private System.Windows.Forms.Label label16;
        private SC_ControlsCS.scTextBoxExt txtMarca;
        private SC_ControlsCS.scComboBoxExt cboPermSCT;
        private System.Windows.Forms.Label lblCancelado;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private SC_ControlsCS.scComboBoxExt cboClaveVehiculo;
    }
}