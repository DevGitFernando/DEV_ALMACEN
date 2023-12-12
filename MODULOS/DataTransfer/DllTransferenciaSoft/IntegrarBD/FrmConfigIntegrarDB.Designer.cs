namespace DllTransferenciaSoft.IntegrarBD
{
    partial class FrmConfigIntegrarDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigIntegrarDB));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIntegrados = new SC_ControlsCS.scTextBoxExt();
            this.cmdRutaIntegracion = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRutaRecibidos = new SC_ControlsCS.scTextBoxExt();
            this.cmdRutaRecibidos = new System.Windows.Forms.Button();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboIntervalos = new SC_ControlsCS.scComboBoxExt();
            this.upDownCada = new System.Windows.Forms.NumericUpDown();
            this.Label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownCada)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtIntegrados);
            this.groupBox4.Controls.Add(this.cmdRutaIntegracion);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtRutaRecibidos);
            this.groupBox4.Controls.Add(this.cmdRutaRecibidos);
            this.groupBox4.Location = new System.Drawing.Point(8, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(601, 115);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configurar rutas";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(537, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Ruta de bases de datos integradas";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIntegrados
            // 
            this.txtIntegrados.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIntegrados.Decimales = 2;
            this.txtIntegrados.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIntegrados.ForeColor = System.Drawing.Color.Black;
            this.txtIntegrados.Location = new System.Drawing.Point(12, 84);
            this.txtIntegrados.MaxLength = 200;
            this.txtIntegrados.Name = "txtIntegrados";
            this.txtIntegrados.PermitirApostrofo = false;
            this.txtIntegrados.PermitirNegativos = false;
            this.txtIntegrados.Size = new System.Drawing.Size(542, 20);
            this.txtIntegrados.TabIndex = 2;
            // 
            // cmdRutaIntegracion
            // 
            this.cmdRutaIntegracion.Location = new System.Drawing.Point(560, 85);
            this.cmdRutaIntegracion.Name = "cmdRutaIntegracion";
            this.cmdRutaIntegracion.Size = new System.Drawing.Size(31, 19);
            this.cmdRutaIntegracion.TabIndex = 3;
            this.cmdRutaIntegracion.Text = "...";
            this.cmdRutaIntegracion.UseVisualStyleBackColor = true;
            this.cmdRutaIntegracion.Click += new System.EventHandler(this.cmdRutaIntegracion_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(537, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Ruta de bases de datos para integración";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRutaRecibidos
            // 
            this.txtRutaRecibidos.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRutaRecibidos.Decimales = 2;
            this.txtRutaRecibidos.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRutaRecibidos.ForeColor = System.Drawing.Color.Black;
            this.txtRutaRecibidos.Location = new System.Drawing.Point(12, 37);
            this.txtRutaRecibidos.MaxLength = 200;
            this.txtRutaRecibidos.Name = "txtRutaRecibidos";
            this.txtRutaRecibidos.PermitirApostrofo = false;
            this.txtRutaRecibidos.PermitirNegativos = false;
            this.txtRutaRecibidos.Size = new System.Drawing.Size(542, 20);
            this.txtRutaRecibidos.TabIndex = 0;
            // 
            // cmdRutaRecibidos
            // 
            this.cmdRutaRecibidos.Location = new System.Drawing.Point(559, 38);
            this.cmdRutaRecibidos.Name = "cmdRutaRecibidos";
            this.cmdRutaRecibidos.Size = new System.Drawing.Size(31, 19);
            this.cmdRutaRecibidos.TabIndex = 1;
            this.cmdRutaRecibidos.Text = "...";
            this.cmdRutaRecibidos.UseVisualStyleBackColor = true;
            this.cmdRutaRecibidos.Click += new System.EventHandler(this.cmdRutaRecibidos_Click);
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(618, 25);
            this.toolStripBarraMenu.TabIndex = 2;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboIntervalos);
            this.groupBox1.Controls.Add(this.upDownCada);
            this.groupBox1.Controls.Add(this.Label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(8, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 50);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurar tiempos";
            // 
            // cboIntervalos
            // 
            this.cboIntervalos.BackColorEnabled = System.Drawing.Color.White;
            this.cboIntervalos.Data = "";
            this.cboIntervalos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIntervalos.Filtro = " 1 = 1";
            this.cboIntervalos.Location = new System.Drawing.Point(149, 19);
            this.cboIntervalos.MostrarToolTip = false;
            this.cboIntervalos.Name = "cboIntervalos";
            this.cboIntervalos.Size = new System.Drawing.Size(139, 21);
            this.cboIntervalos.TabIndex = 3;
            this.cboIntervalos.SelectedIndexChanged += new System.EventHandler(this.cboIntervalos_SelectedIndexChanged);
            // 
            // upDownCada
            // 
            this.upDownCada.Location = new System.Drawing.Point(93, 19);
            this.upDownCada.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.upDownCada.Name = "upDownCada";
            this.upDownCada.Size = new System.Drawing.Size(50, 20);
            this.upDownCada.TabIndex = 2;
            this.upDownCada.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(303, 23);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(208, 13);
            this.Label4.TabIndex = 14;
            this.Label4.Text = "Sí existen bases de datos para integración";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Revisar cada";
            // 
            // FrmConfigIntegrarDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 209);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox4);
            this.Name = "FrmConfigIntegrarDB";
            this.Text = "Configuración de integración de Base de Datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConfigIntegrarDB_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownCada)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.Label label1;
        internal SC_ControlsCS.scTextBoxExt txtRutaRecibidos;
        internal System.Windows.Forms.Button cmdRutaRecibidos;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.Label label2;
        internal SC_ControlsCS.scTextBoxExt txtIntegrados;
        internal System.Windows.Forms.Button cmdRutaIntegracion;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.NumericUpDown upDownCada;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label label5;
        private SC_ControlsCS.scComboBoxExt cboIntervalos;
    }
}