namespace DllTransferenciaSoft.Servicio
{
    partial class FrmLogIntegracion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogIntegracion));
            this.FrameScripts = new System.Windows.Forms.GroupBox();
            this.lstwTablasMigrar = new System.Windows.Forms.ListView();
            this.Tabla = new System.Windows.Forms.ColumnHeader();
            this.Registros = new System.Windows.Forms.ColumnHeader();
            this.RegMigrar = new System.Windows.Forms.ColumnHeader();
            this.Procesada = new System.Windows.Forms.ColumnHeader();
            this.FrameArchivosSII = new System.Windows.Forms.GroupBox();
            this.lblAvance = new System.Windows.Forms.Label();
            this.lstwBasesDeDatos = new System.Windows.Forms.ListView();
            this.Registro = new System.Windows.Forms.ColumnHeader();
            this.ArchivoSII = new System.Windows.Forms.ColumnHeader();
            this.Tamaño = new System.Windows.Forms.ColumnHeader();
            this.Fecha = new System.Windows.Forms.ColumnHeader();
            this.StatusProceso = new System.Windows.Forms.ColumnHeader();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnLogErrores = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tmAvance = new System.Windows.Forms.Timer(this.components);
            this.FrameScripts.SuspendLayout();
            this.FrameArchivosSII.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameScripts
            // 
            this.FrameScripts.Controls.Add(this.lstwTablasMigrar);
            this.FrameScripts.Location = new System.Drawing.Point(828, 62);
            this.FrameScripts.Name = "FrameScripts";
            this.FrameScripts.Size = new System.Drawing.Size(144, 76);
            this.FrameScripts.TabIndex = 3;
            this.FrameScripts.TabStop = false;
            this.FrameScripts.Text = "Listado de archivos";
            // 
            // lstwTablasMigrar
            // 
            this.lstwTablasMigrar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Tabla,
            this.Registros,
            this.RegMigrar,
            this.Procesada});
            this.lstwTablasMigrar.FullRowSelect = true;
            this.lstwTablasMigrar.GridLines = true;
            this.lstwTablasMigrar.Location = new System.Drawing.Point(10, 16);
            this.lstwTablasMigrar.MultiSelect = false;
            this.lstwTablasMigrar.Name = "lstwTablasMigrar";
            this.lstwTablasMigrar.ShowItemToolTips = true;
            this.lstwTablasMigrar.Size = new System.Drawing.Size(782, 51);
            this.lstwTablasMigrar.TabIndex = 1;
            this.lstwTablasMigrar.UseCompatibleStateImageBehavior = false;
            this.lstwTablasMigrar.View = System.Windows.Forms.View.Details;
            // 
            // Tabla
            // 
            this.Tabla.Text = "Tabla";
            this.Tabla.Width = 460;
            // 
            // Registros
            // 
            this.Registros.Text = "Registros";
            this.Registros.Width = 80;
            // 
            // RegMigrar
            // 
            this.RegMigrar.Text = "Migrar";
            this.RegMigrar.Width = 80;
            // 
            // Procesada
            // 
            this.Procesada.Text = "Status";
            this.Procesada.Width = 140;
            // 
            // FrameArchivosSII
            // 
            this.FrameArchivosSII.Controls.Add(this.lblAvance);
            this.FrameArchivosSII.Controls.Add(this.lstwBasesDeDatos);
            this.FrameArchivosSII.Location = new System.Drawing.Point(6, 28);
            this.FrameArchivosSII.Name = "FrameArchivosSII";
            this.FrameArchivosSII.Size = new System.Drawing.Size(782, 416);
            this.FrameArchivosSII.TabIndex = 2;
            this.FrameArchivosSII.TabStop = false;
            this.FrameArchivosSII.Text = "Lista de archivos en integración";
            // 
            // lblAvance
            // 
            this.lblAvance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvance.Location = new System.Drawing.Point(175, -3);
            this.lblAvance.Name = "lblAvance";
            this.lblAvance.Size = new System.Drawing.Size(455, 16);
            this.lblAvance.TabIndex = 1;
            this.lblAvance.Text = "label1";
            this.lblAvance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstwBasesDeDatos
            // 
            this.lstwBasesDeDatos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Registro,
            this.ArchivoSII,
            this.Tamaño,
            this.Fecha,
            this.StatusProceso});
            this.lstwBasesDeDatos.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lstwBasesDeDatos.FullRowSelect = true;
            this.lstwBasesDeDatos.GridLines = true;
            this.lstwBasesDeDatos.Location = new System.Drawing.Point(10, 16);
            this.lstwBasesDeDatos.MultiSelect = false;
            this.lstwBasesDeDatos.Name = "lstwBasesDeDatos";
            this.lstwBasesDeDatos.ShowItemToolTips = true;
            this.lstwBasesDeDatos.Size = new System.Drawing.Size(761, 386);
            this.lstwBasesDeDatos.TabIndex = 0;
            this.lstwBasesDeDatos.UseCompatibleStateImageBehavior = false;
            this.lstwBasesDeDatos.View = System.Windows.Forms.View.Details;
            // 
            // Registro
            // 
            this.Registro.Text = "Núm.";
            // 
            // ArchivoSII
            // 
            this.ArchivoSII.Text = "Archivo";
            this.ArchivoSII.Width = 280;
            // 
            // Tamaño
            // 
            this.Tamaño.Text = "Tamaño KB";
            this.Tamaño.Width = 80;
            // 
            // Fecha
            // 
            this.Fecha.Text = "Fecha ";
            this.Fecha.Width = 140;
            // 
            // StatusProceso
            // 
            this.StatusProceso.Text = "Status";
            this.StatusProceso.Width = 180;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLogErrores,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1166, 25);
            this.toolStripBarraMenu.TabIndex = 4;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnLogErrores
            // 
            this.btnLogErrores.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLogErrores.Image = ((System.Drawing.Image)(resources.GetObject("btnLogErrores.Image")));
            this.btnLogErrores.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogErrores.Name = "btnLogErrores";
            this.btnLogErrores.Size = new System.Drawing.Size(23, 22);
            this.btnLogErrores.Text = "Registro de Errores";
            this.btnLogErrores.ToolTipText = "Registro de Errores";
            this.btnLogErrores.Click += new System.EventHandler(this.btnLogErrores_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tmAvance
            // 
            this.tmAvance.Tick += new System.EventHandler(this.tmAvance_Tick);
            // 
            // FrmLogIntegracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 452);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameScripts);
            this.Controls.Add(this.FrameArchivosSII);
            this.Name = "FrmLogIntegracion";
            this.Text = "Registro de Integración de Información";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmLogIntegracion_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLogIntegracion_FormClosing);
            this.FrameScripts.ResumeLayout(false);
            this.FrameArchivosSII.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameScripts;
        private System.Windows.Forms.ListView lstwTablasMigrar;
        private System.Windows.Forms.ColumnHeader Tabla;
        private System.Windows.Forms.ColumnHeader Registros;
        private System.Windows.Forms.ColumnHeader RegMigrar;
        private System.Windows.Forms.ColumnHeader Procesada;
        private System.Windows.Forms.GroupBox FrameArchivosSII;
        private System.Windows.Forms.ListView lstwBasesDeDatos;
        private System.Windows.Forms.ColumnHeader Registro;
        private System.Windows.Forms.ColumnHeader ArchivoSII;
        private System.Windows.Forms.ColumnHeader Tamaño;
        private System.Windows.Forms.ColumnHeader Fecha;
        private System.Windows.Forms.ColumnHeader StatusProceso;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnLogErrores;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer tmAvance;
        private System.Windows.Forms.Label lblAvance;
    }
}