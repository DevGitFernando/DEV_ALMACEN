namespace DllPedidosClientes.Reportes.Listados
{
	partial class FrmCuadrosBasicosFarmacias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCuadrosBasicosFarmacias));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstClaves = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblClaves = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnExportar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(999, 25);
            this.toolStripBarraMenu.TabIndex = 22;
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
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportar
            // 
            this.btnExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(23, 22);
            this.btnExportar.Text = "&Exportar a Excel";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cboFarmacias);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.cboJurisdicciones);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(12, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(721, 50);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Información de jurisdicciones";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(334, 18);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(371, 21);
            this.cboFarmacias.TabIndex = 32;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(274, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Farmacia :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboJurisdicciones
            // 
            this.cboJurisdicciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdicciones.Data = "";
            this.cboJurisdicciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdicciones.Filtro = " 1 = 1";
            this.cboJurisdicciones.FormattingEnabled = true;
            this.cboJurisdicciones.ListaItemsBusqueda = 20;
            this.cboJurisdicciones.Location = new System.Drawing.Point(79, 18);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(184, 21);
            this.cboJurisdicciones.TabIndex = 30;
            this.cboJurisdicciones.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Jurisdicción :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstClaves);
            this.groupBox1.Location = new System.Drawing.Point(12, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(977, 444);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista de Claves";
            // 
            // lstClaves
            // 
            this.lstClaves.Location = new System.Drawing.Point(7, 19);
            this.lstClaves.Name = "lstClaves";
            this.lstClaves.Size = new System.Drawing.Size(960, 419);
            this.lstClaves.TabIndex = 2;
            this.lstClaves.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblClaves);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(739, 31);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 47);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Información de Claves";
            // 
            // lblClaves
            // 
            this.lblClaves.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaves.Location = new System.Drawing.Point(118, 16);
            this.lblClaves.Name = "lblClaves";
            this.lblClaves.Size = new System.Drawing.Size(122, 21);
            this.lblClaves.TabIndex = 9;
            this.lblClaves.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "Número de claves :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmCuadrosBasicosFarmacias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 540);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmCuadrosBasicosFarmacias";
            this.Text = "Cuadros Basicos de Farmacias";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCuadrosBasicosFarmacias_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstClaves;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblClaves;
        private System.Windows.Forms.Label label12;
	}
}