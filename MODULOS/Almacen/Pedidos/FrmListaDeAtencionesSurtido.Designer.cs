namespace Almacen.Pedidos
{
    partial class FrmListaDeAtencionesSurtido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListaDeAtencionesSurtido));
            this.FramePedidos = new System.Windows.Forms.GroupBox();
            this.lstAtenciones = new System.Windows.Forms.ListView();
            this.Fecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdPersonal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NomPersonal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StatusSurtido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Observaciones = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFolioSurtido = new SC_ControlsCS.scLabelExt();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.FramePedidos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FramePedidos
            // 
            this.FramePedidos.Controls.Add(this.lstAtenciones);
            this.FramePedidos.Location = new System.Drawing.Point(12, 138);
            this.FramePedidos.Margin = new System.Windows.Forms.Padding(4);
            this.FramePedidos.Name = "FramePedidos";
            this.FramePedidos.Padding = new System.Windows.Forms.Padding(4);
            this.FramePedidos.Size = new System.Drawing.Size(1179, 308);
            this.FramePedidos.TabIndex = 10;
            this.FramePedidos.TabStop = false;
            this.FramePedidos.Text = "Listado de Atenciones";
            // 
            // lstAtenciones
            // 
            this.lstAtenciones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Fecha,
            this.IdPersonal,
            this.NomPersonal,
            this.Status,
            this.StatusSurtido,
            this.Observaciones});
            this.lstAtenciones.HideSelection = false;
            this.lstAtenciones.Location = new System.Drawing.Point(9, 20);
            this.lstAtenciones.Margin = new System.Windows.Forms.Padding(4);
            this.lstAtenciones.Name = "lstAtenciones";
            this.lstAtenciones.Size = new System.Drawing.Size(1160, 274);
            this.lstAtenciones.TabIndex = 0;
            this.lstAtenciones.UseCompatibleStateImageBehavior = false;
            this.lstAtenciones.View = System.Windows.Forms.View.Details;
            // 
            // Fecha
            // 
            this.Fecha.Text = "Fecha";
            this.Fecha.Width = 170;
            // 
            // IdPersonal
            // 
            this.IdPersonal.Text = "Núm. Personal";
            this.IdPersonal.Width = 90;
            // 
            // NomPersonal
            // 
            this.NomPersonal.Text = "Nombre Personal";
            this.NomPersonal.Width = 280;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 45;
            // 
            // StatusSurtido
            // 
            this.StatusSurtido.Text = "Status Surtido";
            this.StatusSurtido.Width = 140;
            // 
            // Observaciones
            // 
            this.Observaciones.Text = "Observaciones";
            this.Observaciones.Width = 100;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFolioSurtido);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 72);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1179, 60);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // lblFolioSurtido
            // 
            this.lblFolioSurtido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolioSurtido.Location = new System.Drawing.Point(133, 23);
            this.lblFolioSurtido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFolioSurtido.MostrarToolTip = false;
            this.lblFolioSurtido.Name = "lblFolioSurtido";
            this.lblFolioSurtido.Size = new System.Drawing.Size(163, 25);
            this.lblFolioSurtido.TabIndex = 0;
            this.lblFolioSurtido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 20);
            this.label4.TabIndex = 38;
            this.label4.Text = "Folio de Surtido :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1207, 58);
            this.toolStripBarraMenu.TabIndex = 12;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(54, 55);
            this.btnNuevo.Text = "Nuevo";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 4);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(54, 55);
            this.btnGuardar.Text = "Guardar";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(12, 4);
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(96, 55);
            this.btnSalir.Text = "Salir";
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FrmListaDeAtencionesSurtido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 459);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FramePedidos);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmListaDeAtencionesSurtido";
            this.ShowIcon = false;
            this.Text = "Listado de Atenciones de Surtido";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListaDeAtencionesSurtido_Load);
            this.FramePedidos.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FramePedidos;
        private System.Windows.Forms.ListView lstAtenciones;
        private System.Windows.Forms.ColumnHeader Fecha;
        private System.Windows.Forms.ColumnHeader IdPersonal;
        private System.Windows.Forms.ColumnHeader NomPersonal;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader StatusSurtido;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scLabelExt lblFolioSurtido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader Observaciones;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
    }
}