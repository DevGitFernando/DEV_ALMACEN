namespace Facturacion.GenerarRemisiones
{
    partial class FrmValidarRemisiones_FoliosInvalidos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmValidarRemisiones_FoliosInvalidos));
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.lstwFolios = new System.Windows.Forms.ListView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnQuitarFoliosInvalidos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.FrameFolios.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameFolios
            // 
            this.FrameFolios.Controls.Add(this.lstwFolios);
            this.FrameFolios.Location = new System.Drawing.Point(5, 29);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(892, 409);
            this.FrameFolios.TabIndex = 0;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Detallado";
            // 
            // lstwFolios
            // 
            this.lstwFolios.Location = new System.Drawing.Point(10, 20);
            this.lstwFolios.Name = "lstwFolios";
            this.lstwFolios.Size = new System.Drawing.Size(876, 379);
            this.lstwFolios.TabIndex = 0;
            this.lstwFolios.UseCompatibleStateImageBehavior = false;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGuardar,
            this.toolStripSeparator,
            this.btnQuitarFoliosInvalidos,
            this.toolStripSeparator2,
            this.btnSalir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(909, 25);
            this.toolStripBarraMenu.TabIndex = 1;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnQuitarFoliosInvalidos
            // 
            this.btnQuitarFoliosInvalidos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQuitarFoliosInvalidos.Image = ((System.Drawing.Image)(resources.GetObject("btnQuitarFoliosInvalidos.Image")));
            this.btnQuitarFoliosInvalidos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuitarFoliosInvalidos.Name = "btnQuitarFoliosInvalidos";
            this.btnQuitarFoliosInvalidos.Size = new System.Drawing.Size(23, 22);
            this.btnQuitarFoliosInvalidos.Text = "Validar polizas";
            this.btnQuitarFoliosInvalidos.ToolTipText = "Validar polizas";
            this.btnQuitarFoliosInvalidos.Click += new System.EventHandler(this.btnQuitarFoliosInvalidos_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSalir
            // 
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(23, 22);
            this.btnSalir.Text = "Salir";
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FrmValidarRemisiones_FoliosInvalidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 445);
            this.ControlBox = false;
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameFolios);
            this.Name = "FrmValidarRemisiones_FoliosInvalidos";
            this.Text = "Folios con Formato Inválido de Póliza ";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmValidarRemisiones_FoliosInvalidos_Load);
            this.FrameFolios.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameFolios;
        private System.Windows.Forms.ListView lstwFolios;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnQuitarFoliosInvalidos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSalir;
    }
}