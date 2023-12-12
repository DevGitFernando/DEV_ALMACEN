namespace DllTransferenciaSoft.Configuraciones
{
    partial class FrmConfigObtenerInformacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigObtenerInformacion));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRutaEnviados = new SC_ControlsCS.scTextBoxExt();
            this.cmdRutaEnviados = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdConfigurarProcesoIntegracion = new System.Windows.Forms.Button();
            this.txtRuta = new SC_ControlsCS.scTextBoxExt();
            this.cmdRuta = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkReplicacionPorPeriodo = new System.Windows.Forms.CheckBox();
            this.nmDiasRevision = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.chkServicioHabilitado = new System.Windows.Forms.CheckBox();
            this.dtpFechaTerminacion = new System.Windows.Forms.DateTimePicker();
            this.chkFechaTerminacion = new System.Windows.Forms.CheckBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.chkRepetirProceso = new System.Windows.Forms.CheckBox();
            this.FrameRepetirProceso = new System.Windows.Forms.GroupBox();
            this.cboIntervalos = new SC_ControlsCS.scComboBoxExt();
            this.upDownCada = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpHoraEjecucion = new System.Windows.Forms.DateTimePicker();
            this.Label4 = new System.Windows.Forms.Label();
            this.cboPeriocidad = new SC_ControlsCS.scComboBoxExt();
            this.FrameSemanalmente = new System.Windows.Forms.GroupBox();
            this.chkDomingo = new System.Windows.Forms.CheckBox();
            this.chkSabado = new System.Windows.Forms.CheckBox();
            this.chkViernes = new System.Windows.Forms.CheckBox();
            this.chkJueves = new System.Windows.Forms.CheckBox();
            this.chkMiercoles = new System.Windows.Forms.CheckBox();
            this.chkMartes = new System.Windows.Forms.CheckBox();
            this.chkLunes = new System.Windows.Forms.CheckBox();
            this.upDownSemanas = new System.Windows.Forms.NumericUpDown();
            this.lblSemanasDias = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboUnidadPaquetes = new SC_ControlsCS.scComboBoxExt();
            this.nmTamañoArchivos = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTamaño = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDiasRevision)).BeginInit();
            this.GroupBox3.SuspendLayout();
            this.FrameRepetirProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownCada)).BeginInit();
            this.FrameSemanalmente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSemanas)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmTamañoArchivos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(8, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(621, 460);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Programación del proceso";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtRutaEnviados);
            this.groupBox4.Controls.Add(this.cmdRutaEnviados);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.cmdConfigurarProcesoIntegracion);
            this.groupBox4.Controls.Add(this.txtRuta);
            this.groupBox4.Controls.Add(this.cmdRuta);
            this.groupBox4.Location = new System.Drawing.Point(12, 306);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(596, 144);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configurar rutas";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(246, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Ruta para archivos enviados";
            // 
            // txtRutaEnviados
            // 
            this.txtRutaEnviados.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRutaEnviados.Decimales = 2;
            this.txtRutaEnviados.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRutaEnviados.ForeColor = System.Drawing.Color.Black;
            this.txtRutaEnviados.Location = new System.Drawing.Point(12, 77);
            this.txtRutaEnviados.MaxLength = 200;
            this.txtRutaEnviados.Name = "txtRutaEnviados";
            this.txtRutaEnviados.PermitirApostrofo = false;
            this.txtRutaEnviados.PermitirNegativos = false;
            this.txtRutaEnviados.Size = new System.Drawing.Size(542, 20);
            this.txtRutaEnviados.TabIndex = 2;
            // 
            // cmdRutaEnviados
            // 
            this.cmdRutaEnviados.Location = new System.Drawing.Point(560, 78);
            this.cmdRutaEnviados.Name = "cmdRutaEnviados";
            this.cmdRutaEnviados.Size = new System.Drawing.Size(24, 19);
            this.cmdRutaEnviados.TabIndex = 3;
            this.cmdRutaEnviados.Text = "...";
            this.cmdRutaEnviados.UseVisualStyleBackColor = true;
            this.cmdRutaEnviados.Click += new System.EventHandler(this.cmdRutaEnviados_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Ruta para generación de archivos de información";
            // 
            // cmdConfigurarProcesoIntegracion
            // 
            this.cmdConfigurarProcesoIntegracion.Location = new System.Drawing.Point(393, 110);
            this.cmdConfigurarProcesoIntegracion.Name = "cmdConfigurarProcesoIntegracion";
            this.cmdConfigurarProcesoIntegracion.Size = new System.Drawing.Size(190, 27);
            this.cmdConfigurarProcesoIntegracion.TabIndex = 4;
            this.cmdConfigurarProcesoIntegracion.Text = "Configurar proceso de integración";
            this.cmdConfigurarProcesoIntegracion.UseVisualStyleBackColor = true;
            this.cmdConfigurarProcesoIntegracion.Click += new System.EventHandler(this.cmdConfigurarProcesoIntegracion_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRuta.Decimales = 2;
            this.txtRuta.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRuta.ForeColor = System.Drawing.Color.Black;
            this.txtRuta.Location = new System.Drawing.Point(12, 37);
            this.txtRuta.MaxLength = 200;
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.PermitirApostrofo = false;
            this.txtRuta.PermitirNegativos = false;
            this.txtRuta.Size = new System.Drawing.Size(542, 20);
            this.txtRuta.TabIndex = 0;
            // 
            // cmdRuta
            // 
            this.cmdRuta.Location = new System.Drawing.Point(560, 38);
            this.cmdRuta.Name = "cmdRuta";
            this.cmdRuta.Size = new System.Drawing.Size(24, 19);
            this.cmdRuta.TabIndex = 1;
            this.cmdRuta.Text = "...";
            this.cmdRuta.UseVisualStyleBackColor = true;
            this.cmdRuta.Click += new System.EventHandler(this.cmdRuta_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkReplicacionPorPeriodo);
            this.groupBox2.Controls.Add(this.nmDiasRevision);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.chkServicioHabilitado);
            this.groupBox2.Controls.Add(this.dtpFechaTerminacion);
            this.groupBox2.Controls.Add(this.chkFechaTerminacion);
            this.groupBox2.Controls.Add(this.GroupBox3);
            this.groupBox2.Controls.Add(this.cboPeriocidad);
            this.groupBox2.Controls.Add(this.FrameSemanalmente);
            this.groupBox2.Location = new System.Drawing.Point(12, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(596, 205);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Periodicidad";
            // 
            // chkReplicacionPorPeriodo
            // 
            this.chkReplicacionPorPeriodo.Location = new System.Drawing.Point(207, 173);
            this.chkReplicacionPorPeriodo.Name = "chkReplicacionPorPeriodo";
            this.chkReplicacionPorPeriodo.Size = new System.Drawing.Size(146, 17);
            this.chkReplicacionPorPeriodo.TabIndex = 19;
            this.chkReplicacionPorPeriodo.Text = "Replicar por periodo";
            this.chkReplicacionPorPeriodo.UseVisualStyleBackColor = true;
            // 
            // nmDiasRevision
            // 
            this.nmDiasRevision.Location = new System.Drawing.Point(143, 171);
            this.nmDiasRevision.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nmDiasRevision.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nmDiasRevision.Name = "nmDiasRevision";
            this.nmDiasRevision.Size = new System.Drawing.Size(58, 20);
            this.nmDiasRevision.TabIndex = 18;
            this.nmDiasRevision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmDiasRevision.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Número de dias a revisar ";
            // 
            // chkServicioHabilitado
            // 
            this.chkServicioHabilitado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkServicioHabilitado.Location = new System.Drawing.Point(420, 172);
            this.chkServicioHabilitado.Name = "chkServicioHabilitado";
            this.chkServicioHabilitado.Size = new System.Drawing.Size(163, 18);
            this.chkServicioHabilitado.TabIndex = 16;
            this.chkServicioHabilitado.Text = "Servicio habilitado";
            this.chkServicioHabilitado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkServicioHabilitado.UseVisualStyleBackColor = true;
            // 
            // dtpFechaTerminacion
            // 
            this.dtpFechaTerminacion.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaTerminacion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaTerminacion.Location = new System.Drawing.Point(485, 20);
            this.dtpFechaTerminacion.Name = "dtpFechaTerminacion";
            this.dtpFechaTerminacion.Size = new System.Drawing.Size(98, 20);
            this.dtpFechaTerminacion.TabIndex = 4;
            // 
            // chkFechaTerminacion
            // 
            this.chkFechaTerminacion.AutoSize = true;
            this.chkFechaTerminacion.Location = new System.Drawing.Point(354, 22);
            this.chkFechaTerminacion.Name = "chkFechaTerminacion";
            this.chkFechaTerminacion.Size = new System.Drawing.Size(128, 17);
            this.chkFechaTerminacion.TabIndex = 3;
            this.chkFechaTerminacion.Text = "Fecha de terminación";
            this.chkFechaTerminacion.UseVisualStyleBackColor = true;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.chkRepetirProceso);
            this.GroupBox3.Controls.Add(this.FrameRepetirProceso);
            this.GroupBox3.Controls.Add(this.dtpHoraEjecucion);
            this.GroupBox3.Controls.Add(this.Label4);
            this.GroupBox3.Location = new System.Drawing.Point(359, 46);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(224, 117);
            this.GroupBox3.TabIndex = 2;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Horario de ejecución";
            // 
            // chkRepetirProceso
            // 
            this.chkRepetirProceso.AutoSize = true;
            this.chkRepetirProceso.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chkRepetirProceso.Location = new System.Drawing.Point(11, 51);
            this.chkRepetirProceso.Name = "chkRepetirProceso";
            this.chkRepetirProceso.Size = new System.Drawing.Size(101, 17);
            this.chkRepetirProceso.TabIndex = 1;
            this.chkRepetirProceso.Text = "Repetir proceso";
            this.chkRepetirProceso.UseVisualStyleBackColor = false;
            this.chkRepetirProceso.CheckedChanged += new System.EventHandler(this.chkRepetirProceso_CheckedChanged);
            // 
            // FrameRepetirProceso
            // 
            this.FrameRepetirProceso.Controls.Add(this.cboIntervalos);
            this.FrameRepetirProceso.Controls.Add(this.upDownCada);
            this.FrameRepetirProceso.Controls.Add(this.label5);
            this.FrameRepetirProceso.Location = new System.Drawing.Point(9, 64);
            this.FrameRepetirProceso.Name = "FrameRepetirProceso";
            this.FrameRepetirProceso.Size = new System.Drawing.Size(203, 45);
            this.FrameRepetirProceso.TabIndex = 2;
            this.FrameRepetirProceso.TabStop = false;
            // 
            // cboIntervalos
            // 
            this.cboIntervalos.BackColorEnabled = System.Drawing.Color.White;
            this.cboIntervalos.Data = "";
            this.cboIntervalos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIntervalos.Filtro = " 1 = 1";
            this.cboIntervalos.ListaItemsBusqueda = 20;
            this.cboIntervalos.Location = new System.Drawing.Point(87, 13);
            this.cboIntervalos.MostrarToolTip = false;
            this.cboIntervalos.Name = "cboIntervalos";
            this.cboIntervalos.Size = new System.Drawing.Size(110, 21);
            this.cboIntervalos.TabIndex = 1;
            this.cboIntervalos.SelectedIndexChanged += new System.EventHandler(this.cboIntervalos_SelectedIndexChanged);
            // 
            // upDownCada
            // 
            this.upDownCada.Location = new System.Drawing.Point(45, 13);
            this.upDownCada.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.upDownCada.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownCada.Name = "upDownCada";
            this.upDownCada.Size = new System.Drawing.Size(36, 20);
            this.upDownCada.TabIndex = 0;
            this.upDownCada.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Cada";
            // 
            // dtpHoraEjecucion
            // 
            this.dtpHoraEjecucion.CustomFormat = "H:mm";
            this.dtpHoraEjecucion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraEjecucion.Location = new System.Drawing.Point(117, 22);
            this.dtpHoraEjecucion.Name = "dtpHoraEjecucion";
            this.dtpHoraEjecucion.ShowUpDown = true;
            this.dtpHoraEjecucion.Size = new System.Drawing.Size(89, 20);
            this.dtpHoraEjecucion.TabIndex = 0;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(16, 24);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(94, 13);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "Hora de ejecución";
            // 
            // cboPeriocidad
            // 
            this.cboPeriocidad.BackColorEnabled = System.Drawing.Color.White;
            this.cboPeriocidad.Data = "";
            this.cboPeriocidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPeriocidad.Filtro = " 1 = 1";
            this.cboPeriocidad.ListaItemsBusqueda = 20;
            this.cboPeriocidad.Location = new System.Drawing.Point(15, 19);
            this.cboPeriocidad.MostrarToolTip = false;
            this.cboPeriocidad.Name = "cboPeriocidad";
            this.cboPeriocidad.Size = new System.Drawing.Size(110, 21);
            this.cboPeriocidad.TabIndex = 0;
            this.cboPeriocidad.SelectedIndexChanged += new System.EventHandler(this.cboPeriocidad_SelectedIndexChanged);
            // 
            // FrameSemanalmente
            // 
            this.FrameSemanalmente.Controls.Add(this.chkDomingo);
            this.FrameSemanalmente.Controls.Add(this.chkSabado);
            this.FrameSemanalmente.Controls.Add(this.chkViernes);
            this.FrameSemanalmente.Controls.Add(this.chkJueves);
            this.FrameSemanalmente.Controls.Add(this.chkMiercoles);
            this.FrameSemanalmente.Controls.Add(this.chkMartes);
            this.FrameSemanalmente.Controls.Add(this.chkLunes);
            this.FrameSemanalmente.Controls.Add(this.upDownSemanas);
            this.FrameSemanalmente.Controls.Add(this.lblSemanasDias);
            this.FrameSemanalmente.Controls.Add(this.Label2);
            this.FrameSemanalmente.Location = new System.Drawing.Point(15, 46);
            this.FrameSemanalmente.Name = "FrameSemanalmente";
            this.FrameSemanalmente.Size = new System.Drawing.Size(338, 117);
            this.FrameSemanalmente.TabIndex = 1;
            this.FrameSemanalmente.TabStop = false;
            this.FrameSemanalmente.Text = "Semanalmente";
            // 
            // chkDomingo
            // 
            this.chkDomingo.AutoSize = true;
            this.chkDomingo.Location = new System.Drawing.Point(183, 79);
            this.chkDomingo.Name = "chkDomingo";
            this.chkDomingo.Size = new System.Drawing.Size(68, 17);
            this.chkDomingo.TabIndex = 7;
            this.chkDomingo.Text = "Domingo";
            this.chkDomingo.UseVisualStyleBackColor = true;
            // 
            // chkSabado
            // 
            this.chkSabado.AutoSize = true;
            this.chkSabado.Location = new System.Drawing.Point(114, 79);
            this.chkSabado.Name = "chkSabado";
            this.chkSabado.Size = new System.Drawing.Size(63, 17);
            this.chkSabado.TabIndex = 6;
            this.chkSabado.Text = "Sábado";
            this.chkSabado.UseVisualStyleBackColor = true;
            // 
            // chkViernes
            // 
            this.chkViernes.AutoSize = true;
            this.chkViernes.Location = new System.Drawing.Point(44, 79);
            this.chkViernes.Name = "chkViernes";
            this.chkViernes.Size = new System.Drawing.Size(61, 17);
            this.chkViernes.TabIndex = 5;
            this.chkViernes.Text = "Viernes";
            this.chkViernes.UseVisualStyleBackColor = true;
            // 
            // chkJueves
            // 
            this.chkJueves.AutoSize = true;
            this.chkJueves.Location = new System.Drawing.Point(259, 54);
            this.chkJueves.Name = "chkJueves";
            this.chkJueves.Size = new System.Drawing.Size(60, 17);
            this.chkJueves.TabIndex = 4;
            this.chkJueves.Text = "Jueves";
            this.chkJueves.UseVisualStyleBackColor = true;
            // 
            // chkMiercoles
            // 
            this.chkMiercoles.AutoSize = true;
            this.chkMiercoles.Location = new System.Drawing.Point(183, 54);
            this.chkMiercoles.Name = "chkMiercoles";
            this.chkMiercoles.Size = new System.Drawing.Size(71, 17);
            this.chkMiercoles.TabIndex = 3;
            this.chkMiercoles.Text = "Miércoles";
            this.chkMiercoles.UseVisualStyleBackColor = true;
            // 
            // chkMartes
            // 
            this.chkMartes.AutoSize = true;
            this.chkMartes.Location = new System.Drawing.Point(114, 54);
            this.chkMartes.Name = "chkMartes";
            this.chkMartes.Size = new System.Drawing.Size(58, 17);
            this.chkMartes.TabIndex = 2;
            this.chkMartes.Text = "Martes";
            this.chkMartes.UseVisualStyleBackColor = true;
            // 
            // chkLunes
            // 
            this.chkLunes.AutoSize = true;
            this.chkLunes.Location = new System.Drawing.Point(44, 54);
            this.chkLunes.Name = "chkLunes";
            this.chkLunes.Size = new System.Drawing.Size(55, 17);
            this.chkLunes.TabIndex = 1;
            this.chkLunes.Text = "Lunes";
            this.chkLunes.UseVisualStyleBackColor = true;
            // 
            // upDownSemanas
            // 
            this.upDownSemanas.Location = new System.Drawing.Point(46, 22);
            this.upDownSemanas.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.upDownSemanas.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownSemanas.Name = "upDownSemanas";
            this.upDownSemanas.Size = new System.Drawing.Size(42, 20);
            this.upDownSemanas.TabIndex = 0;
            this.upDownSemanas.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSemanasDias
            // 
            this.lblSemanasDias.AutoSize = true;
            this.lblSemanasDias.Location = new System.Drawing.Point(94, 26);
            this.lblSemanasDias.Name = "lblSemanasDias";
            this.lblSemanasDias.Size = new System.Drawing.Size(62, 13);
            this.lblSemanasDias.TabIndex = 2;
            this.lblSemanasDias.Text = "Semanas el";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(8, 26);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(32, 13);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Cada";
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(638, 25);
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblTamaño);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.cboUnidadPaquetes);
            this.groupBox5.Controls.Add(this.nmTamañoArchivos);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Location = new System.Drawing.Point(12, 223);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(596, 73);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Configuración de envió FTP";
            // 
            // cboUnidadPaquetes
            // 
            this.cboUnidadPaquetes.BackColorEnabled = System.Drawing.Color.White;
            this.cboUnidadPaquetes.Data = "";
            this.cboUnidadPaquetes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnidadPaquetes.Filtro = " 1 = 1";
            this.cboUnidadPaquetes.ListaItemsBusqueda = 20;
            this.cboUnidadPaquetes.Location = new System.Drawing.Point(73, 29);
            this.cboUnidadPaquetes.MostrarToolTip = false;
            this.cboUnidadPaquetes.Name = "cboUnidadPaquetes";
            this.cboUnidadPaquetes.Size = new System.Drawing.Size(115, 21);
            this.cboUnidadPaquetes.TabIndex = 6;
            this.cboUnidadPaquetes.SelectedIndexChanged += new System.EventHandler(this.cboUnidadPaquetes_SelectedIndexChanged);
            // 
            // nmTamañoArchivos
            // 
            this.nmTamañoArchivos.Location = new System.Drawing.Point(267, 29);
            this.nmTamañoArchivos.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nmTamañoArchivos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmTamañoArchivos.Name = "nmTamañoArchivos";
            this.nmTamañoArchivos.Size = new System.Drawing.Size(132, 20);
            this.nmTamañoArchivos.TabIndex = 5;
            this.nmTamañoArchivos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmTamañoArchivos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(202, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Tamaño";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(23, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Unidad ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTamaño
            // 
            this.lblTamaño.Location = new System.Drawing.Point(405, 33);
            this.lblTamaño.Name = "lblTamaño";
            this.lblTamaño.Size = new System.Drawing.Size(178, 13);
            this.lblTamaño.TabIndex = 19;
            this.lblTamaño.Text = "Tamaño";
            this.lblTamaño.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmConfigObtenerInformacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 495);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmConfigObtenerInformacion";
            this.Text = "Configuración de obtención de información";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConfigObtenerInformacion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDiasRevision)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.FrameRepetirProceso.ResumeLayout(false);
            this.FrameRepetirProceso.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownCada)).EndInit();
            this.FrameSemanalmente.ResumeLayout(false);
            this.FrameSemanalmente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSemanas)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmTamañoArchivos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.GroupBox FrameSemanalmente;
        internal System.Windows.Forms.CheckBox chkDomingo;
        internal System.Windows.Forms.CheckBox chkSabado;
        internal System.Windows.Forms.CheckBox chkViernes;
        internal System.Windows.Forms.CheckBox chkJueves;
        internal System.Windows.Forms.CheckBox chkMiercoles;
        internal System.Windows.Forms.CheckBox chkMartes;
        internal System.Windows.Forms.CheckBox chkLunes;
        internal System.Windows.Forms.NumericUpDown upDownSemanas;
        internal System.Windows.Forms.Label lblSemanasDias;
        internal System.Windows.Forms.Label Label2;
        private SC_ControlsCS.scComboBoxExt cboPeriocidad;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.CheckBox chkRepetirProceso;
        internal System.Windows.Forms.GroupBox FrameRepetirProceso;
        private SC_ControlsCS.scComboBoxExt cboIntervalos;
        internal System.Windows.Forms.NumericUpDown upDownCada;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.DateTimePicker dtpHoraEjecucion;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.DateTimePicker dtpFechaTerminacion;
        internal System.Windows.Forms.CheckBox chkFechaTerminacion;
        internal System.Windows.Forms.Button cmdConfigurarProcesoIntegracion;
        internal System.Windows.Forms.Button cmdRuta;
        internal SC_ControlsCS.scTextBoxExt txtRuta;
        private System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label3;
        internal SC_ControlsCS.scTextBoxExt txtRutaEnviados;
        internal System.Windows.Forms.Button cmdRutaEnviados;
        private System.Windows.Forms.CheckBox chkServicioHabilitado;
        internal System.Windows.Forms.NumericUpDown nmDiasRevision;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.CheckBox chkReplicacionPorPeriodo;
        private System.Windows.Forms.GroupBox groupBox5;
        internal System.Windows.Forms.Label label8;
        private SC_ControlsCS.scComboBoxExt cboUnidadPaquetes;
        internal System.Windows.Forms.NumericUpDown nmTamañoArchivos;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label lblTamaño;
    }
}