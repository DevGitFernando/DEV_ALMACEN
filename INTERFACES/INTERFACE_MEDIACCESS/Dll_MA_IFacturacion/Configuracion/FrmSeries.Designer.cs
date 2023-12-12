namespace Dll_MA_IFacturacion.Configuracion
{
    partial class FrmSeries
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSeries));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFolioUtilizado = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFolioFinal = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFolioInicial = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboTipoDoctos = new SC_ControlsCS.scComboBoxExt();
            this.txtSerie = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFolioUtilizado);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtFolioFinal);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtFolioInicial);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(8, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(405, 90);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rango de Folios";
            // 
            // txtFolioUtilizado
            // 
            this.txtFolioUtilizado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioUtilizado.Decimales = 2;
            this.txtFolioUtilizado.Enabled = false;
            this.txtFolioUtilizado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioUtilizado.ForeColor = System.Drawing.Color.Black;
            this.txtFolioUtilizado.Location = new System.Drawing.Point(144, 61);
            this.txtFolioUtilizado.MaxLength = 8;
            this.txtFolioUtilizado.Name = "txtFolioUtilizado";
            this.txtFolioUtilizado.PermitirApostrofo = false;
            this.txtFolioUtilizado.PermitirNegativos = false;
            this.txtFolioUtilizado.Size = new System.Drawing.Size(128, 20);
            this.txtFolioUtilizado.TabIndex = 2;
            this.txtFolioUtilizado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 20);
            this.label3.TabIndex = 45;
            this.label3.Text = "Último Folio Utilizado :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioFinal
            // 
            this.txtFolioFinal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioFinal.Decimales = 2;
            this.txtFolioFinal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioFinal.ForeColor = System.Drawing.Color.Black;
            this.txtFolioFinal.Location = new System.Drawing.Point(144, 38);
            this.txtFolioFinal.MaxLength = 8;
            this.txtFolioFinal.Name = "txtFolioFinal";
            this.txtFolioFinal.PermitirApostrofo = false;
            this.txtFolioFinal.PermitirNegativos = false;
            this.txtFolioFinal.Size = new System.Drawing.Size(128, 20);
            this.txtFolioFinal.TabIndex = 1;
            this.txtFolioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolioFinal.TextChanged += new System.EventHandler(this.txtFolioFinal_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 43;
            this.label4.Text = "Final :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioInicial
            // 
            this.txtFolioInicial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioInicial.Decimales = 2;
            this.txtFolioInicial.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioInicial.ForeColor = System.Drawing.Color.Black;
            this.txtFolioInicial.Location = new System.Drawing.Point(144, 15);
            this.txtFolioInicial.MaxLength = 8;
            this.txtFolioInicial.Name = "txtFolioInicial";
            this.txtFolioInicial.PermitirApostrofo = false;
            this.txtFolioInicial.PermitirNegativos = false;
            this.txtFolioInicial.Size = new System.Drawing.Size(128, 20);
            this.txtFolioInicial.TabIndex = 0;
            this.txtFolioInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolioInicial.TextChanged += new System.EventHandler(this.txtFolioInicial_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(15, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 20);
            this.label5.TabIndex = 41;
            this.label5.Text = "Inicial :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(420, 25);
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
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkStatus);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboTipoDoctos);
            this.groupBox1.Controls.Add(this.txtSerie);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 78);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serie y Nombre Documento";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(15, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 21);
            this.label7.TabIndex = 49;
            this.label7.Text = "Tipo del Documento :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoDoctos
            // 
            this.cboTipoDoctos.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoDoctos.Data = "";
            this.cboTipoDoctos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoDoctos.Filtro = " 1 = 1";
            this.cboTipoDoctos.FormattingEnabled = true;
            this.cboTipoDoctos.ListaItemsBusqueda = 20;
            this.cboTipoDoctos.Location = new System.Drawing.Point(144, 47);
            this.cboTipoDoctos.MostrarToolTip = false;
            this.cboTipoDoctos.Name = "cboTipoDoctos";
            this.cboTipoDoctos.Size = new System.Drawing.Size(248, 21);
            this.cboTipoDoctos.TabIndex = 4;
            // 
            // txtSerie
            // 
            this.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSerie.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSerie.Decimales = 2;
            this.txtSerie.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtSerie.ForeColor = System.Drawing.Color.Black;
            this.txtSerie.Location = new System.Drawing.Point(144, 22);
            this.txtSerie.MaxLength = 10;
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.PermitirApostrofo = false;
            this.txtSerie.PermitirNegativos = false;
            this.txtSerie.Size = new System.Drawing.Size(128, 20);
            this.txtSerie.TabIndex = 3;
            this.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 20);
            this.label2.TabIndex = 45;
            this.label2.Text = "Serie :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkStatus
            // 
            this.chkStatus.Location = new System.Drawing.Point(285, 24);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(114, 17);
            this.chkStatus.TabIndex = 50;
            this.chkStatus.Text = "Activa";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // FrmSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 205);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSeries";
            this.Text = "Folios y Series";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private SC_ControlsCS.scTextBoxExt txtFolioUtilizado;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtFolioFinal;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtFolioInicial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtSerie;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scComboBoxExt cboTipoDoctos;
        private System.Windows.Forms.CheckBox chkStatus;
    }
}