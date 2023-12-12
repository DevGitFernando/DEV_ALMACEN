namespace DllPedidosClientes.ReportesCentral.Existencias
{
    partial class FrmExistenciaPorClave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExistenciaPorClave));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstClaves = new SC_ControlsCS.scListView();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.tmSesion = new System.Windows.Forms.Timer(this.components);
            this.FrameInsumos = new System.Windows.Forms.GroupBox();
            this.rdoInsumoMatCuracion = new System.Windows.Forms.RadioButton();
            this.rdoInsumosAmbos = new System.Windows.Forms.RadioButton();
            this.rdoInsumosMedicamento = new System.Windows.Forms.RadioButton();
            this.FrameDispensacion = new System.Windows.Forms.GroupBox();
            this.rdoTpDispAmbos = new System.Windows.Forms.RadioButton();
            this.rdoTpDispConsignacion = new System.Windows.Forms.RadioButton();
            this.rdoTpDispVenta = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoRptSinExist = new System.Windows.Forms.RadioButton();
            this.rdoRptTodos = new System.Windows.Forms.RadioButton();
            this.rdoRptConExist = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoClaveNoCauses = new System.Windows.Forms.RadioButton();
            this.rdoClaveAmbos = new System.Windows.Forms.RadioButton();
            this.rdoClaveCauses = new System.Windows.Forms.RadioButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.FrameInsumos.SuspendLayout();
            this.FrameDispensacion.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(767, 25);
            this.toolStripBarraMenu.TabIndex = 1;
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
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Enabled = false;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cboFarmacias);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.cboJurisdicciones);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(6, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(752, 50);
            this.groupBox5.TabIndex = 26;
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
            this.cboFarmacias.Location = new System.Drawing.Point(403, 18);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(339, 21);
            this.cboFarmacias.TabIndex = 32;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(344, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
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
            this.cboJurisdicciones.Location = new System.Drawing.Point(81, 18);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(248, 21);
            this.cboJurisdicciones.TabIndex = 30;
            this.cboJurisdicciones.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Jurisdicción :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstClaves);
            this.groupBox3.Location = new System.Drawing.Point(6, 174);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(753, 378);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Unidades";
            // 
            // lstClaves
            // 
            this.lstClaves.Location = new System.Drawing.Point(12, 19);
            this.lstClaves.LockColumnSize = false;
            this.lstClaves.Name = "lstClaves";
            this.lstClaves.Size = new System.Drawing.Size(728, 345);
            this.lstClaves.TabIndex = 6;
            this.lstClaves.UseCompatibleStateImageBehavior = false;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            // 
            // tmSesion
            // 
            this.tmSesion.Tick += new System.EventHandler(this.tmSesion_Tick);
            // 
            // FrameInsumos
            // 
            this.FrameInsumos.Controls.Add(this.rdoInsumoMatCuracion);
            this.FrameInsumos.Controls.Add(this.rdoInsumosAmbos);
            this.FrameInsumos.Controls.Add(this.rdoInsumosMedicamento);
            this.FrameInsumos.Location = new System.Drawing.Point(6, 78);
            this.FrameInsumos.Name = "FrameInsumos";
            this.FrameInsumos.Size = new System.Drawing.Size(374, 47);
            this.FrameInsumos.TabIndex = 27;
            this.FrameInsumos.TabStop = false;
            this.FrameInsumos.Text = "Tipo de Insumo";
            // 
            // rdoInsumoMatCuracion
            // 
            this.rdoInsumoMatCuracion.Location = new System.Drawing.Point(225, 19);
            this.rdoInsumoMatCuracion.Name = "rdoInsumoMatCuracion";
            this.rdoInsumoMatCuracion.Size = new System.Drawing.Size(124, 15);
            this.rdoInsumoMatCuracion.TabIndex = 3;
            this.rdoInsumoMatCuracion.TabStop = true;
            this.rdoInsumoMatCuracion.Text = "Material de curación";
            this.rdoInsumoMatCuracion.UseVisualStyleBackColor = true;
            this.rdoInsumoMatCuracion.CheckedChanged += new System.EventHandler(this.rdoInsumoMatCuracion_CheckedChanged);
            // 
            // rdoInsumosAmbos
            // 
            this.rdoInsumosAmbos.Checked = true;
            this.rdoInsumosAmbos.Location = new System.Drawing.Point(25, 19);
            this.rdoInsumosAmbos.Name = "rdoInsumosAmbos";
            this.rdoInsumosAmbos.Size = new System.Drawing.Size(64, 15);
            this.rdoInsumosAmbos.TabIndex = 0;
            this.rdoInsumosAmbos.TabStop = true;
            this.rdoInsumosAmbos.Text = "Ambos";
            this.rdoInsumosAmbos.UseVisualStyleBackColor = true;
            this.rdoInsumosAmbos.CheckedChanged += new System.EventHandler(this.rdoInsumosAmbos_CheckedChanged);
            // 
            // rdoInsumosMedicamento
            // 
            this.rdoInsumosMedicamento.Location = new System.Drawing.Point(113, 19);
            this.rdoInsumosMedicamento.Name = "rdoInsumosMedicamento";
            this.rdoInsumosMedicamento.Size = new System.Drawing.Size(122, 15);
            this.rdoInsumosMedicamento.TabIndex = 2;
            this.rdoInsumosMedicamento.TabStop = true;
            this.rdoInsumosMedicamento.Text = "Medicamento";
            this.rdoInsumosMedicamento.UseVisualStyleBackColor = true;
            this.rdoInsumosMedicamento.CheckedChanged += new System.EventHandler(this.rdoInsumosMedicamento_CheckedChanged);
            // 
            // FrameDispensacion
            // 
            this.FrameDispensacion.Controls.Add(this.rdoTpDispAmbos);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispConsignacion);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispVenta);
            this.FrameDispensacion.Location = new System.Drawing.Point(384, 78);
            this.FrameDispensacion.Name = "FrameDispensacion";
            this.FrameDispensacion.Size = new System.Drawing.Size(374, 47);
            this.FrameDispensacion.TabIndex = 28;
            this.FrameDispensacion.TabStop = false;
            this.FrameDispensacion.Text = "Tipo de Dispensación";
            // 
            // rdoTpDispAmbos
            // 
            this.rdoTpDispAmbos.Checked = true;
            this.rdoTpDispAmbos.Location = new System.Drawing.Point(34, 19);
            this.rdoTpDispAmbos.Name = "rdoTpDispAmbos";
            this.rdoTpDispAmbos.Size = new System.Drawing.Size(94, 15);
            this.rdoTpDispAmbos.TabIndex = 0;
            this.rdoTpDispAmbos.TabStop = true;
            this.rdoTpDispAmbos.Text = "Ambos";
            this.rdoTpDispAmbos.UseVisualStyleBackColor = true;
            this.rdoTpDispAmbos.CheckedChanged += new System.EventHandler(this.rdoTpDispAmbos_CheckedChanged);
            // 
            // rdoTpDispConsignacion
            // 
            this.rdoTpDispConsignacion.Location = new System.Drawing.Point(247, 19);
            this.rdoTpDispConsignacion.Name = "rdoTpDispConsignacion";
            this.rdoTpDispConsignacion.Size = new System.Drawing.Size(94, 17);
            this.rdoTpDispConsignacion.TabIndex = 2;
            this.rdoTpDispConsignacion.TabStop = true;
            this.rdoTpDispConsignacion.Text = "Consignación";
            this.rdoTpDispConsignacion.UseVisualStyleBackColor = true;
            this.rdoTpDispConsignacion.CheckedChanged += new System.EventHandler(this.rdoTpDispConsignacion_CheckedChanged);
            // 
            // rdoTpDispVenta
            // 
            this.rdoTpDispVenta.Location = new System.Drawing.Point(131, 19);
            this.rdoTpDispVenta.Name = "rdoTpDispVenta";
            this.rdoTpDispVenta.Size = new System.Drawing.Size(94, 15);
            this.rdoTpDispVenta.TabIndex = 1;
            this.rdoTpDispVenta.TabStop = true;
            this.rdoTpDispVenta.Text = "Venta";
            this.rdoTpDispVenta.UseVisualStyleBackColor = true;
            this.rdoTpDispVenta.CheckedChanged += new System.EventHandler(this.rdoTpDispVenta_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoRptSinExist);
            this.groupBox1.Controls.Add(this.rdoRptTodos);
            this.groupBox1.Controls.Add(this.rdoRptConExist);
            this.groupBox1.Location = new System.Drawing.Point(6, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 47);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Existencias";
            // 
            // rdoRptSinExist
            // 
            this.rdoRptSinExist.AutoSize = true;
            this.rdoRptSinExist.Location = new System.Drawing.Point(225, 16);
            this.rdoRptSinExist.Name = "rdoRptSinExist";
            this.rdoRptSinExist.Size = new System.Drawing.Size(91, 17);
            this.rdoRptSinExist.TabIndex = 2;
            this.rdoRptSinExist.Text = "Sin Existencia";
            this.rdoRptSinExist.UseVisualStyleBackColor = true;
            this.rdoRptSinExist.CheckedChanged += new System.EventHandler(this.rdoRptSinExist_CheckedChanged);
            // 
            // rdoRptTodos
            // 
            this.rdoRptTodos.AutoSize = true;
            this.rdoRptTodos.Checked = true;
            this.rdoRptTodos.Location = new System.Drawing.Point(25, 16);
            this.rdoRptTodos.Name = "rdoRptTodos";
            this.rdoRptTodos.Size = new System.Drawing.Size(55, 17);
            this.rdoRptTodos.TabIndex = 0;
            this.rdoRptTodos.TabStop = true;
            this.rdoRptTodos.Text = "Todos";
            this.rdoRptTodos.UseVisualStyleBackColor = true;
            this.rdoRptTodos.CheckedChanged += new System.EventHandler(this.rdoRptTodos_CheckedChanged);
            // 
            // rdoRptConExist
            // 
            this.rdoRptConExist.AutoSize = true;
            this.rdoRptConExist.Location = new System.Drawing.Point(113, 18);
            this.rdoRptConExist.Name = "rdoRptConExist";
            this.rdoRptConExist.Size = new System.Drawing.Size(95, 17);
            this.rdoRptConExist.TabIndex = 1;
            this.rdoRptConExist.Text = "Con Existencia";
            this.rdoRptConExist.UseVisualStyleBackColor = true;
            this.rdoRptConExist.CheckedChanged += new System.EventHandler(this.rdoRptConExist_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoClaveNoCauses);
            this.groupBox2.Controls.Add(this.rdoClaveAmbos);
            this.groupBox2.Controls.Add(this.rdoClaveCauses);
            this.groupBox2.Location = new System.Drawing.Point(384, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(374, 47);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de Clave";
            // 
            // rdoClaveNoCauses
            // 
            this.rdoClaveNoCauses.AutoSize = true;
            this.rdoClaveNoCauses.Location = new System.Drawing.Point(247, 16);
            this.rdoClaveNoCauses.Name = "rdoClaveNoCauses";
            this.rdoClaveNoCauses.Size = new System.Drawing.Size(77, 17);
            this.rdoClaveNoCauses.TabIndex = 2;
            this.rdoClaveNoCauses.Text = "No Causes";
            this.rdoClaveNoCauses.UseVisualStyleBackColor = true;
            this.rdoClaveNoCauses.CheckedChanged += new System.EventHandler(this.rdoClaveNoCauses_CheckedChanged);
            // 
            // rdoClaveAmbos
            // 
            this.rdoClaveAmbos.AutoSize = true;
            this.rdoClaveAmbos.Checked = true;
            this.rdoClaveAmbos.Location = new System.Drawing.Point(34, 16);
            this.rdoClaveAmbos.Name = "rdoClaveAmbos";
            this.rdoClaveAmbos.Size = new System.Drawing.Size(55, 17);
            this.rdoClaveAmbos.TabIndex = 0;
            this.rdoClaveAmbos.TabStop = true;
            this.rdoClaveAmbos.Text = "Todos";
            this.rdoClaveAmbos.UseVisualStyleBackColor = true;
            this.rdoClaveAmbos.CheckedChanged += new System.EventHandler(this.rdoClaveAmbos_CheckedChanged);
            // 
            // rdoClaveCauses
            // 
            this.rdoClaveCauses.AutoSize = true;
            this.rdoClaveCauses.Location = new System.Drawing.Point(131, 18);
            this.rdoClaveCauses.Name = "rdoClaveCauses";
            this.rdoClaveCauses.Size = new System.Drawing.Size(60, 17);
            this.rdoClaveCauses.TabIndex = 1;
            this.rdoClaveCauses.Text = "Causes";
            this.rdoClaveCauses.UseVisualStyleBackColor = true;
            this.rdoClaveCauses.CheckedChanged += new System.EventHandler(this.rdoClaveCauses_CheckedChanged);
            // 
            // FrmExistenciaPorClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 560);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameDispensacion);
            this.Controls.Add(this.FrameInsumos);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmExistenciaPorClave";
            this.Text = "Existencia por Clave SSA";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmExistenciaPorClave_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.FrameInsumos.ResumeLayout(false);
            this.FrameDispensacion.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private SC_ControlsCS.scListView lstClaves;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.Timer tmSesion;
        private System.Windows.Forms.GroupBox FrameInsumos;
        private System.Windows.Forms.RadioButton rdoInsumoMatCuracion;
        private System.Windows.Forms.RadioButton rdoInsumosAmbos;
        private System.Windows.Forms.RadioButton rdoInsumosMedicamento;
        private System.Windows.Forms.GroupBox FrameDispensacion;
        private System.Windows.Forms.RadioButton rdoTpDispAmbos;
        private System.Windows.Forms.RadioButton rdoTpDispConsignacion;
        private System.Windows.Forms.RadioButton rdoTpDispVenta;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoRptSinExist;
        private System.Windows.Forms.RadioButton rdoRptTodos;
        private System.Windows.Forms.RadioButton rdoRptConExist;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoClaveNoCauses;
        private System.Windows.Forms.RadioButton rdoClaveAmbos;
        private System.Windows.Forms.RadioButton rdoClaveCauses;
    }
}