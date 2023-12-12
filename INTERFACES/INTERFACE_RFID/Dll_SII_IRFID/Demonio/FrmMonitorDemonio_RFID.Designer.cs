namespace Dll_SII_IRFID.Monitor
{
    partial class FrmMonitorDemonio_RFID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMonitorDemonio_RFID));
            this.label2 = new System.Windows.Forms.Label();
            this.FramePedidos = new System.Windows.Forms.GroupBox();
            this.listvwPedidos = new System.Windows.Forms.ListView();
            this.colLector = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClaveSSA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescripcionClaveSSA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCodigoEAN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescripcionComercial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExistencia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnFuente = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.lblTiempoActualizacion = new System.Windows.Forms.ToolStripLabel();
            this.tmCuentaRegresiva = new System.Windows.Forms.Timer(this.components);
            this.tmInformacion = new System.Windows.Forms.Timer(this.components);
            this.lblTotalPiezas = new SC_ControlsCS.scLabelExt();
            this.btnApagarGPO = new System.Windows.Forms.Button();
            this.tmGPO = new System.Windows.Forms.Timer(this.components);
            this.FramePedidos.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(0, 398);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(977, 24);
            this.label2.TabIndex = 18;
            this.label2.Text = "<F5> Actualizar";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FramePedidos
            // 
            this.FramePedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FramePedidos.Controls.Add(this.listvwPedidos);
            this.FramePedidos.Location = new System.Drawing.Point(11, 28);
            this.FramePedidos.Name = "FramePedidos";
            this.FramePedidos.Size = new System.Drawing.Size(959, 364);
            this.FramePedidos.TabIndex = 17;
            this.FramePedidos.TabStop = false;
            this.FramePedidos.Text = "Listado de Claves";
            // 
            // listvwPedidos
            // 
            this.listvwPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listvwPedidos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLector,
            this.colClaveSSA,
            this.colDescripcionClaveSSA,
            this.colCodigoEAN,
            this.colDescripcionComercial,
            this.colExistencia});
            this.listvwPedidos.FullRowSelect = true;
            this.listvwPedidos.Location = new System.Drawing.Point(10, 16);
            this.listvwPedidos.MultiSelect = false;
            this.listvwPedidos.Name = "listvwPedidos";
            this.listvwPedidos.Size = new System.Drawing.Size(940, 340);
            this.listvwPedidos.TabIndex = 0;
            this.listvwPedidos.UseCompatibleStateImageBehavior = false;
            this.listvwPedidos.View = System.Windows.Forms.View.Details;
            // 
            // colLector
            // 
            this.colLector.Text = "Área";
            this.colLector.Width = 120;
            // 
            // colClaveSSA
            // 
            this.colClaveSSA.Text = "Clave SSA";
            this.colClaveSSA.Width = 150;
            // 
            // colDescripcionClaveSSA
            // 
            this.colDescripcionClaveSSA.Text = "Descripción Clave SSA";
            this.colDescripcionClaveSSA.Width = 330;
            // 
            // colCodigoEAN
            // 
            this.colCodigoEAN.Text = "Código EAN";
            this.colCodigoEAN.Width = 100;
            // 
            // colDescripcionComercial
            // 
            this.colDescripcionComercial.Text = "Descripción comercial";
            this.colDescripcionComercial.Width = 200;
            // 
            // colExistencia
            // 
            this.colExistencia.Text = "Existencia";
            this.colExistencia.Width = 120;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFuente,
            this.toolStripSeparator,
            this.lblTiempoActualizacion});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(977, 25);
            this.toolStrip.TabIndex = 16;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnFuente
            // 
            this.btnFuente.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFuente.Image = ((System.Drawing.Image)(resources.GetObject("btnFuente.Image")));
            this.btnFuente.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFuente.Name = "btnFuente";
            this.btnFuente.Size = new System.Drawing.Size(47, 22);
            this.btnFuente.Text = "Fuente";
            this.btnFuente.Click += new System.EventHandler(this.btnFuente_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // lblTiempoActualizacion
            // 
            this.lblTiempoActualizacion.Name = "lblTiempoActualizacion";
            this.lblTiempoActualizacion.Size = new System.Drawing.Size(103, 22);
            this.lblTiempoActualizacion.Text = "Actualización en : ";
            // 
            // tmCuentaRegresiva
            // 
            this.tmCuentaRegresiva.Interval = 1000;
            this.tmCuentaRegresiva.Tick += new System.EventHandler(this.tmCuentaRegresiva_Tick);
            // 
            // tmInformacion
            // 
            this.tmInformacion.Tick += new System.EventHandler(this.tmInformacion_Tick);
            // 
            // lblTotalPiezas
            // 
            this.lblTotalPiezas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalPiezas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalPiezas.Location = new System.Drawing.Point(703, 3);
            this.lblTotalPiezas.MostrarToolTip = false;
            this.lblTotalPiezas.Name = "lblTotalPiezas";
            this.lblTotalPiezas.Size = new System.Drawing.Size(258, 18);
            this.lblTotalPiezas.TabIndex = 19;
            this.lblTotalPiezas.Text = "scLabelExt1";
            this.lblTotalPiezas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnApagarGPO
            // 
            this.btnApagarGPO.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnApagarGPO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApagarGPO.Location = new System.Drawing.Point(338, 1);
            this.btnApagarGPO.Name = "btnApagarGPO";
            this.btnApagarGPO.Size = new System.Drawing.Size(301, 22);
            this.btnApagarGPO.TabIndex = 20;
            this.btnApagarGPO.Text = "Se detectaron Tags inválidos";
            this.btnApagarGPO.UseVisualStyleBackColor = true;
            this.btnApagarGPO.Click += new System.EventHandler(this.btnApagarGPO_Click);
            // 
            // tmGPO
            // 
            this.tmGPO.Tick += new System.EventHandler(this.tmGPO_Tick);
            // 
            // FrmMonitorDemonio_RFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 422);
            this.Controls.Add(this.btnApagarGPO);
            this.Controls.Add(this.lblTotalPiezas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FramePedidos);
            this.Controls.Add(this.toolStrip);
            this.Name = "FrmMonitorDemonio_RFID";
            this.Text = "Monitor RFDI";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMonitorDemonio_RFID_FormClosing);
            this.Load += new System.EventHandler(this.FrmMonitorDemonio_RFID_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMonitorSurtimientoDePedidos_KeyDown);
            this.FramePedidos.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FramePedidos;
        private System.Windows.Forms.ListView listvwPedidos;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnFuente;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripLabel lblTiempoActualizacion;
        private System.Windows.Forms.Timer tmCuentaRegresiva;
        private System.Windows.Forms.ColumnHeader colClaveSSA;
        private System.Windows.Forms.ColumnHeader colDescripcionClaveSSA;
        private System.Windows.Forms.ColumnHeader colExistencia;
        private System.Windows.Forms.Timer tmInformacion;
        private SC_ControlsCS.scLabelExt lblTotalPiezas;
        private System.Windows.Forms.ColumnHeader colCodigoEAN;
        private System.Windows.Forms.ColumnHeader colDescripcionComercial;
        private System.Windows.Forms.ColumnHeader colLector;
        private System.Windows.Forms.Button btnApagarGPO;
        private System.Windows.Forms.Timer tmGPO;

    }
}