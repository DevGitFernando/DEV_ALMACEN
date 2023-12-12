namespace DllRegistrosSanitarios.Sincronizar
{
    partial class FrmSincronizarDatos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSincronizarDatos));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnSincronizarDatos = new System.Windows.Forms.ToolStripButton();
            this.btnSincronizarSQLite = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAvance = new System.Windows.Forms.Label();
            this.listViewRegistros = new System.Windows.Forms.ListView();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(45, 45);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnSincronizarDatos,
            this.btnSincronizarSQLite,
            this.btnValidarDatos});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1226, 52);
            this.toolStrip.TabIndex = 28;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(49, 49);
            this.btnNuevo.Text = "Sincronizar datos";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnSincronizarDatos
            // 
            this.btnSincronizarDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSincronizarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnSincronizarDatos.Image")));
            this.btnSincronizarDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSincronizarDatos.Name = "btnSincronizarDatos";
            this.btnSincronizarDatos.Size = new System.Drawing.Size(49, 49);
            this.btnSincronizarDatos.Text = "Sincronizar datos";
            this.btnSincronizarDatos.Click += new System.EventHandler(this.btnSincronizarDatos_Click);
            // 
            // btnSincronizarSQLite
            // 
            this.btnSincronizarSQLite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSincronizarSQLite.Image = ((System.Drawing.Image)(resources.GetObject("btnSincronizarSQLite.Image")));
            this.btnSincronizarSQLite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSincronizarSQLite.Name = "btnSincronizarSQLite";
            this.btnSincronizarSQLite.Size = new System.Drawing.Size(49, 49);
            this.btnSincronizarSQLite.Text = "Sincronizar datos";
            this.btnSincronizarSQLite.Click += new System.EventHandler(this.btnSincronizarSQLite_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAvance);
            this.groupBox1.Controls.Add(this.listViewRegistros);
            this.groupBox1.Location = new System.Drawing.Point(11, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1202, 595);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            // 
            // lblAvance
            // 
            this.lblAvance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAvance.Location = new System.Drawing.Point(15, 17);
            this.lblAvance.Name = "lblAvance";
            this.lblAvance.Size = new System.Drawing.Size(1174, 26);
            this.lblAvance.TabIndex = 31;
            this.lblAvance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listViewRegistros
            // 
            this.listViewRegistros.Location = new System.Drawing.Point(15, 47);
            this.listViewRegistros.Name = "listViewRegistros";
            this.listViewRegistros.Size = new System.Drawing.Size(1174, 524);
            this.listViewRegistros.TabIndex = 30;
            this.listViewRegistros.UseCompatibleStateImageBehavior = false;
            // 
            // FrmSincronizarDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 665);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip);
            this.Name = "FrmSincronizarDatos";
            this.Text = "Sincronizar datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmSincronizarDatos_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnSincronizarDatos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewRegistros;
        private System.Windows.Forms.ToolStripButton btnSincronizarSQLite;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.Label lblAvance;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
    }
}