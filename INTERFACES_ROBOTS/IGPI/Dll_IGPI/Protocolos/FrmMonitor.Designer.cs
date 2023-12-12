namespace Dll_IGPI.Protocolos
{
    partial class FrmMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMonitor));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tabMensajes = new System.Windows.Forms.TabControl();
            this.tabMensajesEntrada = new System.Windows.Forms.TabPage();
            this.tabMensajesSalida = new System.Windows.Forms.TabPage();
            this.lstMsjEntrada = new System.Windows.Forms.ListView();
            this.colEntradaRegistro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEntradaMensaje = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstMsjSalida = new System.Windows.Forms.ListView();
            this.colSalidaRegistro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSalidaMensaje = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu.SuspendLayout();
            this.tabMensajes.SuspendLayout();
            this.tabMensajesEntrada.SuspendLayout();
            this.tabMensajesSalida.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(801, 25);
            this.toolStripBarraMenu.TabIndex = 10;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
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
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // tabMensajes
            // 
            this.tabMensajes.Controls.Add(this.tabMensajesEntrada);
            this.tabMensajes.Controls.Add(this.tabMensajesSalida);
            this.tabMensajes.Location = new System.Drawing.Point(10, 28);
            this.tabMensajes.Name = "tabMensajes";
            this.tabMensajes.SelectedIndex = 0;
            this.tabMensajes.Size = new System.Drawing.Size(784, 394);
            this.tabMensajes.TabIndex = 11;
            // 
            // tabMensajesEntrada
            // 
            this.tabMensajesEntrada.Controls.Add(this.lstMsjEntrada);
            this.tabMensajesEntrada.Location = new System.Drawing.Point(4, 22);
            this.tabMensajesEntrada.Name = "tabMensajesEntrada";
            this.tabMensajesEntrada.Padding = new System.Windows.Forms.Padding(3);
            this.tabMensajesEntrada.Size = new System.Drawing.Size(776, 368);
            this.tabMensajesEntrada.TabIndex = 0;
            this.tabMensajesEntrada.Text = "Mensajes de entrada";
            this.tabMensajesEntrada.UseVisualStyleBackColor = true;
            // 
            // tabMensajesSalida
            // 
            this.tabMensajesSalida.Controls.Add(this.lstMsjSalida);
            this.tabMensajesSalida.Location = new System.Drawing.Point(4, 22);
            this.tabMensajesSalida.Name = "tabMensajesSalida";
            this.tabMensajesSalida.Padding = new System.Windows.Forms.Padding(3);
            this.tabMensajesSalida.Size = new System.Drawing.Size(776, 368);
            this.tabMensajesSalida.TabIndex = 1;
            this.tabMensajesSalida.Text = "Mensajes de salida";
            this.tabMensajesSalida.UseVisualStyleBackColor = true;
            // 
            // lstMsjEntrada
            // 
            this.lstMsjEntrada.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEntradaRegistro,
            this.colEntradaMensaje});
            this.lstMsjEntrada.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMsjEntrada.Location = new System.Drawing.Point(3, 3);
            this.lstMsjEntrada.Name = "lstMsjEntrada";
            this.lstMsjEntrada.Size = new System.Drawing.Size(770, 362);
            this.lstMsjEntrada.TabIndex = 1;
            this.lstMsjEntrada.UseCompatibleStateImageBehavior = false;
            this.lstMsjEntrada.View = System.Windows.Forms.View.Details;
            // 
            // colEntradaRegistro
            // 
            this.colEntradaRegistro.Text = "Registro";
            this.colEntradaRegistro.Width = 100;
            // 
            // colEntradaMensaje
            // 
            this.colEntradaMensaje.Text = "Mensaje";
            this.colEntradaMensaje.Width = 650;
            // 
            // lstMsjSalida
            // 
            this.lstMsjSalida.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSalidaRegistro,
            this.colSalidaMensaje});
            this.lstMsjSalida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMsjSalida.Location = new System.Drawing.Point(3, 3);
            this.lstMsjSalida.Name = "lstMsjSalida";
            this.lstMsjSalida.Size = new System.Drawing.Size(770, 362);
            this.lstMsjSalida.TabIndex = 2;
            this.lstMsjSalida.UseCompatibleStateImageBehavior = false;
            this.lstMsjSalida.View = System.Windows.Forms.View.Details;
            // 
            // colSalidaRegistro
            // 
            this.colSalidaRegistro.Text = "Registro";
            this.colSalidaRegistro.Width = 100;
            // 
            // colSalidaMensaje
            // 
            this.colSalidaMensaje.Text = "Mensaje";
            this.colSalidaMensaje.Width = 650;
            // 
            // FrmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 428);
            this.Controls.Add(this.tabMensajes);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmMonitor";
            this.Text = "Monitor";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.tabMensajes.ResumeLayout(false);
            this.tabMensajesEntrada.ResumeLayout(false);
            this.tabMensajesSalida.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabMensajes;
        private System.Windows.Forms.TabPage tabMensajesEntrada;
        private System.Windows.Forms.TabPage tabMensajesSalida;
        private System.Windows.Forms.ListView lstMsjEntrada;
        private System.Windows.Forms.ColumnHeader colEntradaRegistro;
        private System.Windows.Forms.ColumnHeader colEntradaMensaje;
        private System.Windows.Forms.ListView lstMsjSalida;
        private System.Windows.Forms.ColumnHeader colSalidaRegistro;
        private System.Windows.Forms.ColumnHeader colSalidaMensaje;
    }
}