namespace Farmacia.Inventario.Reubicaciones
{
    partial class FrmRptReubicaciones_Monitor
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
            this.tmRefrescar = new System.Windows.Forms.Timer(this.components);
            this.FrameResultado = new System.Windows.Forms.GroupBox();
            this.lstVwInformacion = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.lblTiempoActualizacion = new System.Windows.Forms.ToolStripLabel();
            this.tmCuentaRegresiva = new System.Windows.Forms.Timer(this.components);
            this.tmActualizarInformacion = new System.Windows.Forms.Timer(this.components);
            this.FrameResultado.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmRefrescar
            // 
            this.tmRefrescar.Interval = 900000;
            this.tmRefrescar.Tick += new System.EventHandler(this.tmRefrescar_Tick);
            // 
            // FrameResultado
            // 
            this.FrameResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameResultado.Controls.Add(this.lstVwInformacion);
            this.FrameResultado.Location = new System.Drawing.Point(12, 28);
            this.FrameResultado.Name = "FrameResultado";
            this.FrameResultado.Size = new System.Drawing.Size(963, 409);
            this.FrameResultado.TabIndex = 13;
            this.FrameResultado.TabStop = false;
            this.FrameResultado.Text = "Información";
            // 
            // lstVwInformacion
            // 
            this.lstVwInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwInformacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstVwInformacion.Location = new System.Drawing.Point(10, 18);
            this.lstVwInformacion.Name = "lstVwInformacion";
            this.lstVwInformacion.Size = new System.Drawing.Size(943, 380);
            this.lstVwInformacion.TabIndex = 0;
            this.lstVwInformacion.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(0, 445);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(983, 24);
            this.label2.TabIndex = 14;
            this.label2.Text = "<F5> Actualizar";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator,
            this.lblTiempoActualizacion});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(983, 25);
            this.toolStrip.TabIndex = 15;
            this.toolStrip.Text = "toolStrip1";
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
            // tmActualizarInformacion
            // 
            this.tmActualizarInformacion.Tick += new System.EventHandler(this.tmActualizarInformacion_Tick);
            // 
            // FrmRptReubicaciones_Monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 469);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FrameResultado);
            this.Name = "FrmRptReubicaciones_Monitor";
            this.Text = "Monitor de Reubicaciones";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRptReubicaciones_Monitor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRptReubicaciones_Monitor_KeyDown);
            this.FrameResultado.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmRefrescar;
        private System.Windows.Forms.GroupBox FrameResultado;
        private System.Windows.Forms.ListView lstVwInformacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripLabel lblTiempoActualizacion;
        private System.Windows.Forms.Timer tmCuentaRegresiva;
        private System.Windows.Forms.Timer tmActualizarInformacion;
    }
}