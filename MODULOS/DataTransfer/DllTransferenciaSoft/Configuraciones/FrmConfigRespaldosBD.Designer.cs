namespace DllTransferenciaSoft.Configuraciones
{
    partial class FrmConfigRespaldosBD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigRespaldosBD));
            this.cboIntervalos = new SC_ControlsCS.scComboBoxExt();
            this.upDownCada = new System.Windows.Forms.NumericUpDown();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkServicioHabilitado = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombreServidor = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRutaRespaldos = new SC_ControlsCS.scTextBoxExt();
            this.cmdRutaRecibidos = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRutaSistema = new SC_ControlsCS.scTextBoxExt();
            this.btnRutaSistema = new System.Windows.Forms.Button();
            this.dtpHoraInicial = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.FrameDiasEjecucion = new System.Windows.Forms.GroupBox();
            this.chkDomingo = new System.Windows.Forms.CheckBox();
            this.chkSabado = new System.Windows.Forms.CheckBox();
            this.chkViernes = new System.Windows.Forms.CheckBox();
            this.chkJueves = new System.Windows.Forms.CheckBox();
            this.chkMiercoles = new System.Windows.Forms.CheckBox();
            this.chkMartes = new System.Windows.Forms.CheckBox();
            this.chkLunes = new System.Windows.Forms.CheckBox();
            this.dtpHoraFinal = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.chkEnvioFTP = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.upDownCada)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameDiasEjecucion.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboIntervalos
            // 
            this.cboIntervalos.BackColorEnabled = System.Drawing.Color.White;
            this.cboIntervalos.Data = "";
            this.cboIntervalos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIntervalos.Filtro = " 1 = 1";
            this.cboIntervalos.ListaItemsBusqueda = 20;
            this.cboIntervalos.Location = new System.Drawing.Point(153, 62);
            this.cboIntervalos.MostrarToolTip = false;
            this.cboIntervalos.Name = "cboIntervalos";
            this.cboIntervalos.Size = new System.Drawing.Size(139, 21);
            this.cboIntervalos.TabIndex = 3;
            this.cboIntervalos.SelectedIndexChanged += new System.EventHandler(this.cboIntervalos_SelectedIndexChanged);
            // 
            // upDownCada
            // 
            this.upDownCada.Location = new System.Drawing.Point(97, 62);
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
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(616, 25);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkServicioHabilitado);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.cboIntervalos);
            this.groupBox4.Controls.Add(this.upDownCada);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtRutaRespaldos);
            this.groupBox4.Controls.Add(this.cmdRutaRecibidos);
            this.groupBox4.Location = new System.Drawing.Point(12, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(596, 95);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configurar rutas";
            // 
            // chkServicioHabilitado
            // 
            this.chkServicioHabilitado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkServicioHabilitado.Location = new System.Drawing.Point(424, 65);
            this.chkServicioHabilitado.Name = "chkServicioHabilitado";
            this.chkServicioHabilitado.Size = new System.Drawing.Size(163, 18);
            this.chkServicioHabilitado.TabIndex = 4;
            this.chkServicioHabilitado.Text = "Servicio habilitado";
            this.chkServicioHabilitado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkServicioHabilitado.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(538, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nombre publico del servidor";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNombreServidor
            // 
            this.txtNombreServidor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombreServidor.Decimales = 2;
            this.txtNombreServidor.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombreServidor.ForeColor = System.Drawing.Color.Black;
            this.txtNombreServidor.Location = new System.Drawing.Point(15, 80);
            this.txtNombreServidor.MaxLength = 100;
            this.txtNombreServidor.Name = "txtNombreServidor";
            this.txtNombreServidor.PermitirApostrofo = false;
            this.txtNombreServidor.PermitirNegativos = false;
            this.txtNombreServidor.Size = new System.Drawing.Size(538, 20);
            this.txtNombreServidor.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ejecutar cada :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(538, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ruta para respaldos generados";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRutaRespaldos
            // 
            this.txtRutaRespaldos.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRutaRespaldos.Decimales = 2;
            this.txtRutaRespaldos.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRutaRespaldos.ForeColor = System.Drawing.Color.Black;
            this.txtRutaRespaldos.Location = new System.Drawing.Point(15, 36);
            this.txtRutaRespaldos.MaxLength = 200;
            this.txtRutaRespaldos.Name = "txtRutaRespaldos";
            this.txtRutaRespaldos.PermitirApostrofo = false;
            this.txtRutaRespaldos.PermitirNegativos = false;
            this.txtRutaRespaldos.Size = new System.Drawing.Size(538, 20);
            this.txtRutaRespaldos.TabIndex = 0;
            // 
            // cmdRutaRecibidos
            // 
            this.cmdRutaRecibidos.Location = new System.Drawing.Point(556, 37);
            this.cmdRutaRecibidos.Name = "cmdRutaRecibidos";
            this.cmdRutaRecibidos.Size = new System.Drawing.Size(31, 19);
            this.cmdRutaRecibidos.TabIndex = 1;
            this.cmdRutaRecibidos.Text = "...";
            this.cmdRutaRecibidos.UseVisualStyleBackColor = true;
            this.cmdRutaRecibidos.Click += new System.EventHandler(this.cmdRutaRecibidos_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNombreServidor);
            this.groupBox1.Controls.Add(this.txtRutaSistema);
            this.groupBox1.Controls.Add(this.btnRutaSistema);
            this.groupBox1.Location = new System.Drawing.Point(165, 226);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 114);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registro de rutas de Sistema";
            this.groupBox1.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(538, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ruta de archivos de Sistema";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRutaSistema
            // 
            this.txtRutaSistema.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRutaSistema.Decimales = 2;
            this.txtRutaSistema.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRutaSistema.ForeColor = System.Drawing.Color.Black;
            this.txtRutaSistema.Location = new System.Drawing.Point(15, 38);
            this.txtRutaSistema.MaxLength = 200;
            this.txtRutaSistema.Name = "txtRutaSistema";
            this.txtRutaSistema.PermitirApostrofo = false;
            this.txtRutaSistema.PermitirNegativos = false;
            this.txtRutaSistema.Size = new System.Drawing.Size(538, 20);
            this.txtRutaSistema.TabIndex = 1;
            // 
            // btnRutaSistema
            // 
            this.btnRutaSistema.Location = new System.Drawing.Point(556, 39);
            this.btnRutaSistema.Name = "btnRutaSistema";
            this.btnRutaSistema.Size = new System.Drawing.Size(31, 19);
            this.btnRutaSistema.TabIndex = 2;
            this.btnRutaSistema.Text = "...";
            this.btnRutaSistema.UseVisualStyleBackColor = true;
            this.btnRutaSistema.Click += new System.EventHandler(this.btnRutaSistema_Click);
            // 
            // dtpHoraInicial
            // 
            this.dtpHoraInicial.CustomFormat = "H:mm";
            this.dtpHoraInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraInicial.Location = new System.Drawing.Point(528, 20);
            this.dtpHoraInicial.Name = "dtpHoraInicial";
            this.dtpHoraInicial.ShowUpDown = true;
            this.dtpHoraInicial.Size = new System.Drawing.Size(59, 20);
            this.dtpHoraInicial.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(431, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Hora inicio :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDiasEjecucion
            // 
            this.FrameDiasEjecucion.Controls.Add(this.chkEnvioFTP);
            this.FrameDiasEjecucion.Controls.Add(this.dtpHoraFinal);
            this.FrameDiasEjecucion.Controls.Add(this.chkDomingo);
            this.FrameDiasEjecucion.Controls.Add(this.label5);
            this.FrameDiasEjecucion.Controls.Add(this.dtpHoraInicial);
            this.FrameDiasEjecucion.Controls.Add(this.chkSabado);
            this.FrameDiasEjecucion.Controls.Add(this.label6);
            this.FrameDiasEjecucion.Controls.Add(this.chkViernes);
            this.FrameDiasEjecucion.Controls.Add(this.chkJueves);
            this.FrameDiasEjecucion.Controls.Add(this.chkMiercoles);
            this.FrameDiasEjecucion.Controls.Add(this.chkMartes);
            this.FrameDiasEjecucion.Controls.Add(this.chkLunes);
            this.FrameDiasEjecucion.Location = new System.Drawing.Point(12, 125);
            this.FrameDiasEjecucion.Name = "FrameDiasEjecucion";
            this.FrameDiasEjecucion.Size = new System.Drawing.Size(596, 74);
            this.FrameDiasEjecucion.TabIndex = 2;
            this.FrameDiasEjecucion.TabStop = false;
            this.FrameDiasEjecucion.Text = "Enviar a FTP";
            // 
            // chkDomingo
            // 
            this.chkDomingo.Location = new System.Drawing.Point(281, 45);
            this.chkDomingo.Name = "chkDomingo";
            this.chkDomingo.Size = new System.Drawing.Size(71, 17);
            this.chkDomingo.TabIndex = 7;
            this.chkDomingo.Text = "Domingo";
            this.chkDomingo.UseVisualStyleBackColor = true;
            // 
            // chkSabado
            // 
            this.chkSabado.Location = new System.Drawing.Point(201, 45);
            this.chkSabado.Name = "chkSabado";
            this.chkSabado.Size = new System.Drawing.Size(71, 17);
            this.chkSabado.TabIndex = 6;
            this.chkSabado.Text = "Sábado";
            this.chkSabado.UseVisualStyleBackColor = true;
            // 
            // chkViernes
            // 
            this.chkViernes.Location = new System.Drawing.Point(121, 45);
            this.chkViernes.Name = "chkViernes";
            this.chkViernes.Size = new System.Drawing.Size(71, 17);
            this.chkViernes.TabIndex = 5;
            this.chkViernes.Text = "Viernes";
            this.chkViernes.UseVisualStyleBackColor = true;
            // 
            // chkJueves
            // 
            this.chkJueves.Location = new System.Drawing.Point(361, 22);
            this.chkJueves.Name = "chkJueves";
            this.chkJueves.Size = new System.Drawing.Size(71, 17);
            this.chkJueves.TabIndex = 4;
            this.chkJueves.Text = "Jueves";
            this.chkJueves.UseVisualStyleBackColor = true;
            // 
            // chkMiercoles
            // 
            this.chkMiercoles.Location = new System.Drawing.Point(281, 22);
            this.chkMiercoles.Name = "chkMiercoles";
            this.chkMiercoles.Size = new System.Drawing.Size(71, 17);
            this.chkMiercoles.TabIndex = 3;
            this.chkMiercoles.Text = "Miércoles";
            this.chkMiercoles.UseVisualStyleBackColor = true;
            // 
            // chkMartes
            // 
            this.chkMartes.Location = new System.Drawing.Point(201, 22);
            this.chkMartes.Name = "chkMartes";
            this.chkMartes.Size = new System.Drawing.Size(71, 17);
            this.chkMartes.TabIndex = 2;
            this.chkMartes.Text = "Martes";
            this.chkMartes.UseVisualStyleBackColor = true;
            // 
            // chkLunes
            // 
            this.chkLunes.Location = new System.Drawing.Point(121, 22);
            this.chkLunes.Name = "chkLunes";
            this.chkLunes.Size = new System.Drawing.Size(71, 17);
            this.chkLunes.TabIndex = 1;
            this.chkLunes.Text = "Lunes";
            this.chkLunes.UseVisualStyleBackColor = true;
            // 
            // dtpHoraFinal
            // 
            this.dtpHoraFinal.CustomFormat = "H:mm";
            this.dtpHoraFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraFinal.Location = new System.Drawing.Point(528, 43);
            this.dtpHoraFinal.Name = "dtpHoraFinal";
            this.dtpHoraFinal.ShowUpDown = true;
            this.dtpHoraFinal.Size = new System.Drawing.Size(59, 20);
            this.dtpHoraFinal.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(431, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Hora finaliza :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkEnvioFTP
            // 
            this.chkEnvioFTP.Location = new System.Drawing.Point(15, 21);
            this.chkEnvioFTP.Name = "chkEnvioFTP";
            this.chkEnvioFTP.Size = new System.Drawing.Size(81, 18);
            this.chkEnvioFTP.TabIndex = 0;
            this.chkEnvioFTP.Text = "Habilitado";
            this.chkEnvioFTP.UseVisualStyleBackColor = true;
            // 
            // FrmConfigRespaldosBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 206);
            this.Controls.Add(this.FrameDiasEjecucion);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox4);
            this.Name = "FrmConfigRespaldosBD";
            this.Text = "Configuración de respaldos de Base de Datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConfigRespaldosBD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.upDownCada)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameDiasEjecucion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SC_ControlsCS.scComboBoxExt cboIntervalos;
        internal System.Windows.Forms.NumericUpDown upDownCada;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.Label label1;
        internal SC_ControlsCS.scTextBoxExt txtRutaRespaldos;
        internal System.Windows.Forms.Button cmdRutaRecibidos;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal SC_ControlsCS.scTextBoxExt txtNombreServidor;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Label label4;
        internal SC_ControlsCS.scTextBoxExt txtRutaSistema;
        internal System.Windows.Forms.Button btnRutaSistema;
        private System.Windows.Forms.CheckBox chkServicioHabilitado;
        internal System.Windows.Forms.DateTimePicker dtpHoraInicial;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.GroupBox FrameDiasEjecucion;
        internal System.Windows.Forms.CheckBox chkDomingo;
        internal System.Windows.Forms.CheckBox chkSabado;
        internal System.Windows.Forms.CheckBox chkViernes;
        internal System.Windows.Forms.CheckBox chkJueves;
        internal System.Windows.Forms.CheckBox chkMiercoles;
        internal System.Windows.Forms.CheckBox chkMartes;
        internal System.Windows.Forms.CheckBox chkLunes;
        internal System.Windows.Forms.DateTimePicker dtpHoraFinal;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkEnvioFTP;
    }
}