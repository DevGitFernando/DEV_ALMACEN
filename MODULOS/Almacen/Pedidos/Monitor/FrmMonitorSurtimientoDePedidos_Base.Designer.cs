namespace Almacen.Pedidos
{
    partial class FrmMonitorSurtimientoDePedidos_Base
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMonitorSurtimientoDePedidos_Base));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnFuente = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.lblTiempoActualizacion = new System.Windows.Forms.ToolStripLabel();
            this.FramePedidos = new System.Windows.Forms.GroupBox();
            this.listvwPedidos = new System.Windows.Forms.ListView();
            this.colPrioridad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPrioridadDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipoPedido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdJurisdiccion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colJurisdiccion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdFarmaciaPedido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFarmaciaPedido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolioPedido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFechaEntrega = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFechaPedido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolioSurtido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatusPedido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tmCuentaRegresiva = new System.Windows.Forms.Timer(this.components);
            this.tmActualizarInformacion = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip.SuspendLayout();
            this.FramePedidos.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStrip.TabIndex = 5;
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
            // FramePedidos
            // 
            this.FramePedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FramePedidos.Controls.Add(this.listvwPedidos);
            this.FramePedidos.Location = new System.Drawing.Point(11, 28);
            this.FramePedidos.Name = "FramePedidos";
            this.FramePedidos.Size = new System.Drawing.Size(959, 364);
            this.FramePedidos.TabIndex = 11;
            this.FramePedidos.TabStop = false;
            this.FramePedidos.Text = "Listado de Pedidos y Surtidos";
            // 
            // listvwPedidos
            // 
            this.listvwPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listvwPedidos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPrioridad,
            this.colPrioridadDesc,
            this.colTipoPedido,
            this.colIdJurisdiccion,
            this.colJurisdiccion,
            this.colIdFarmaciaPedido,
            this.colFarmaciaPedido,
            this.colFolioPedido,
            this.colFechaEntrega,
            this.colFechaPedido,
            this.colFolioSurtido,
            this.colStatusPedido});
            this.listvwPedidos.FullRowSelect = true;
            this.listvwPedidos.HideSelection = false;
            this.listvwPedidos.Location = new System.Drawing.Point(10, 16);
            this.listvwPedidos.MultiSelect = false;
            this.listvwPedidos.Name = "listvwPedidos";
            this.listvwPedidos.Size = new System.Drawing.Size(940, 340);
            this.listvwPedidos.TabIndex = 0;
            this.listvwPedidos.UseCompatibleStateImageBehavior = false;
            this.listvwPedidos.View = System.Windows.Forms.View.Details;
            // 
            // colPrioridad
            // 
            this.colPrioridad.Text = "Prioridad";
            // 
            // colPrioridadDesc
            // 
            this.colPrioridadDesc.Text = "Descripción";
            // 
            // colTipoPedido
            // 
            this.colTipoPedido.Text = "Tipo de pedido";
            this.colTipoPedido.Width = 80;
            // 
            // colIdJurisdiccion
            // 
            this.colIdJurisdiccion.Text = "Núm. Jurisdicción";
            this.colIdJurisdiccion.Width = 110;
            // 
            // colJurisdiccion
            // 
            this.colJurisdiccion.Text = "Jurisdicción";
            this.colJurisdiccion.Width = 87;
            // 
            // colIdFarmaciaPedido
            // 
            this.colIdFarmaciaPedido.Text = "Núm. Farmacia Pedido";
            this.colIdFarmaciaPedido.Width = 128;
            // 
            // colFarmaciaPedido
            // 
            this.colFarmaciaPedido.Text = "Farmacia Pedido";
            this.colFarmaciaPedido.Width = 118;
            // 
            // colFolioPedido
            // 
            this.colFolioPedido.Text = "Folio Pedido";
            this.colFolioPedido.Width = 93;
            // 
            // colFechaEntrega
            // 
            this.colFechaEntrega.Text = "Fecha Entrega";
            this.colFechaEntrega.Width = 100;
            // 
            // colFechaPedido
            // 
            this.colFechaPedido.Text = "Fecha Pedido";
            this.colFechaPedido.Width = 91;
            // 
            // colFolioSurtido
            // 
            this.colFolioSurtido.Text = "Folio Surtido";
            this.colFolioSurtido.Width = 78;
            // 
            // colStatusPedido
            // 
            this.colStatusPedido.Text = "Status Pedido";
            this.colStatusPedido.Width = 85;
            // 
            // tmCuentaRegresiva
            // 
            this.tmCuentaRegresiva.Interval = 1000;
            this.tmCuentaRegresiva.Tick += new System.EventHandler(this.tmCuentaRegresiva_Tick);
            // 
            // tmActualizarInformacion
            // 
            this.tmActualizarInformacion.Tick += new System.EventHandler(this.tmActualizarInformacion_Tick);
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
            this.label2.TabIndex = 15;
            this.label2.Text = "<F5> Actualizar";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmMonitorSurtimientoDePedidos_Base
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 422);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FramePedidos);
            this.Controls.Add(this.toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMonitorSurtimientoDePedidos_Base";
            this.Text = "Monitor de surtido de pedidos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmMonitorSurtimientoDePedidos_Base_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMonitorSurtimientoDePedidos_Base_KeyDown);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.FramePedidos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.GroupBox FramePedidos;
        private System.Windows.Forms.ListView listvwPedidos;
        private System.Windows.Forms.ColumnHeader colIdJurisdiccion;
        private System.Windows.Forms.ColumnHeader colJurisdiccion;
        private System.Windows.Forms.ColumnHeader colIdFarmaciaPedido;
        private System.Windows.Forms.ColumnHeader colFarmaciaPedido;
        private System.Windows.Forms.ColumnHeader colFolioPedido;
        private System.Windows.Forms.ColumnHeader colFechaEntrega;
        private System.Windows.Forms.ColumnHeader colFechaPedido;
        private System.Windows.Forms.ColumnHeader colFolioSurtido;
        private System.Windows.Forms.ColumnHeader colStatusPedido;
        private System.Windows.Forms.Timer tmCuentaRegresiva;
        private System.Windows.Forms.Timer tmActualizarInformacion;
        private System.Windows.Forms.ToolStripLabel lblTiempoActualizacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton btnFuente;
        private System.Windows.Forms.ColumnHeader colTipoPedido;
        private System.Windows.Forms.ColumnHeader colPrioridad;
        private System.Windows.Forms.ColumnHeader colPrioridadDesc;
    }
}