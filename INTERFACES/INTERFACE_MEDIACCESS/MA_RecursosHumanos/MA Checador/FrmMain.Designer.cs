namespace MA_Checador
{
	partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.BarraDeStatus = new System.Windows.Forms.StatusBar();
            this.lblModulo = new System.Windows.Forms.StatusBarPanel();
            this.lblServidor = new System.Windows.Forms.StatusBarPanel();
            this.lblBaseDeDatos = new System.Windows.Forms.StatusBarPanel();
            this.lblUsuarioConectado = new System.Windows.Forms.StatusBarPanel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnEntrada = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalida = new System.Windows.Forms.ToolStripButton();
            this.tmAutenticacion = new System.Windows.Forms.Timer(this.components);
            this.splitContainer_Gral = new System.Windows.Forms.SplitContainer();
            this.lblTitulo_Checador = new System.Windows.Forms.Label();
            this.lblProcesoIntegracion = new System.Windows.Forms.Label();
            this.tmSincronizacion = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEnviarRegistroChecador = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDescargarHuellas = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.lblModulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblServidor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseDeDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUsuarioConectado)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.splitContainer_Gral.Panel1.SuspendLayout();
            this.splitContainer_Gral.Panel2.SuspendLayout();
            this.splitContainer_Gral.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BarraDeStatus
            // 
            this.BarraDeStatus.Location = new System.Drawing.Point(0, 323);
            this.BarraDeStatus.Name = "BarraDeStatus";
            this.BarraDeStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.lblModulo,
            this.lblServidor,
            this.lblBaseDeDatos,
            this.lblUsuarioConectado});
            this.BarraDeStatus.ShowPanels = true;
            this.BarraDeStatus.Size = new System.Drawing.Size(1097, 26);
            this.BarraDeStatus.TabIndex = 28;
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblModulo.MinWidth = 15;
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Width = 15;
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Width = 10;
            // 
            // lblBaseDeDatos
            // 
            this.lblBaseDeDatos.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblBaseDeDatos.Name = "lblBaseDeDatos";
            this.lblBaseDeDatos.Width = 10;
            // 
            // lblUsuarioConectado
            // 
            this.lblUsuarioConectado.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblUsuarioConectado.Name = "lblUsuarioConectado";
            this.lblUsuarioConectado.Width = 10;
            // 
            // toolStrip
            // 
            this.toolStrip.AllowMerge = false;
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.SystemColors.Window;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(60, 60);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEntrada,
            this.toolStripSeparator1,
            this.btnSalida});
            this.toolStrip.Location = new System.Drawing.Point(0, 23);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(746, 67);
            this.toolStrip.TabIndex = 27;
            this.toolStrip.Text = "Menú";
            // 
            // btnEntrada
            // 
            this.btnEntrada.AutoSize = false;
            this.btnEntrada.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrada.Image = global::MA_Checador.Properties.Resources.icono_entrada;
            this.btnEntrada.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEntrada.Name = "btnEntrada";
            this.btnEntrada.Size = new System.Drawing.Size(60, 60);
            this.btnEntrada.Text = "ENTRADA";
            this.btnEntrada.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEntrada.Click += new System.EventHandler(this.btnEntrada_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 67);
            // 
            // btnSalida
            // 
            this.btnSalida.AutoSize = false;
            this.btnSalida.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalida.Image = global::MA_Checador.Properties.Resources.icono_salida;
            this.btnSalida.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalida.Name = "btnSalida";
            this.btnSalida.Size = new System.Drawing.Size(60, 60);
            this.btnSalida.Text = "SALIDA";
            this.btnSalida.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalida.Click += new System.EventHandler(this.btnSalida_Click);
            // 
            // tmAutenticacion
            // 
            this.tmAutenticacion.Interval = 2000;
            this.tmAutenticacion.Tick += new System.EventHandler(this.tmAutenticacion_Tick);
            // 
            // splitContainer_Gral
            // 
            this.splitContainer_Gral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer_Gral.Location = new System.Drawing.Point(28, 12);
            this.splitContainer_Gral.Name = "splitContainer_Gral";
            // 
            // splitContainer_Gral.Panel1
            // 
            this.splitContainer_Gral.Panel1.Controls.Add(this.toolStrip);
            this.splitContainer_Gral.Panel1.Controls.Add(this.lblTitulo_Checador);
            // 
            // splitContainer_Gral.Panel2
            // 
            this.splitContainer_Gral.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer_Gral.Panel2.Controls.Add(this.label1);
            this.splitContainer_Gral.Panel2MinSize = 50;
            this.splitContainer_Gral.Size = new System.Drawing.Size(988, 122);
            this.splitContainer_Gral.SplitterDistance = 750;
            this.splitContainer_Gral.TabIndex = 30;
            // 
            // lblTitulo_Checador
            // 
            this.lblTitulo_Checador.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo_Checador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo_Checador.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo_Checador.Name = "lblTitulo_Checador";
            this.lblTitulo_Checador.Size = new System.Drawing.Size(746, 23);
            this.lblTitulo_Checador.TabIndex = 28;
            this.lblTitulo_Checador.Text = "CHECADOR";
            this.lblTitulo_Checador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProcesoIntegracion
            // 
            this.lblProcesoIntegracion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblProcesoIntegracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcesoIntegracion.Location = new System.Drawing.Point(0, 300);
            this.lblProcesoIntegracion.Name = "lblProcesoIntegracion";
            this.lblProcesoIntegracion.Size = new System.Drawing.Size(1097, 23);
            this.lblProcesoIntegracion.TabIndex = 31;
            this.lblProcesoIntegracion.Text = "CHECADOR";
            this.lblProcesoIntegracion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProcesoIntegracion.Visible = false;
            // 
            // tmSincronizacion
            // 
            this.tmSincronizacion.Interval = 15000;
            this.tmSincronizacion.Tick += new System.EventHandler(this.tmSincronizacion_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Window;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(60, 60);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDescargarHuellas,
            this.toolStripSeparator2,
            this.btnEnviarRegistroChecador});
            this.toolStrip1.Location = new System.Drawing.Point(0, 23);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(230, 67);
            this.toolStrip1.TabIndex = 29;
            this.toolStrip1.Text = "Menú";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 23);
            this.label1.TabIndex = 30;
            this.label1.Text = "SINCRONIZAR";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEnviarRegistroChecador
            // 
            this.btnEnviarRegistroChecador.AutoSize = false;
            this.btnEnviarRegistroChecador.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarRegistroChecador.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviarRegistroChecador.Image")));
            this.btnEnviarRegistroChecador.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEnviarRegistroChecador.Name = "btnEnviarRegistroChecador";
            this.btnEnviarRegistroChecador.Size = new System.Drawing.Size(60, 60);
            this.btnEnviarRegistroChecador.Text = "DATOS";
            this.btnEnviarRegistroChecador.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEnviarRegistroChecador.Click += new System.EventHandler(this.btnEnviarRegistroChecador_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 67);
            // 
            // btnDescargarHuellas
            // 
            this.btnDescargarHuellas.AutoSize = false;
            this.btnDescargarHuellas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescargarHuellas.Image = ((System.Drawing.Image)(resources.GetObject("btnDescargarHuellas.Image")));
            this.btnDescargarHuellas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDescargarHuellas.Name = "btnDescargarHuellas";
            this.btnDescargarHuellas.Size = new System.Drawing.Size(60, 60);
            this.btnDescargarHuellas.Text = "HUELLAS";
            this.btnDescargarHuellas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDescargarHuellas.Click += new System.EventHandler(this.btnDescargarHuellas_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 349);
            this.Controls.Add(this.lblProcesoIntegracion);
            this.Controls.Add(this.splitContainer_Gral);
            this.Controls.Add(this.BarraDeStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.Text = "Checador";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.lblModulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblServidor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseDeDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUsuarioConectado)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer_Gral.Panel1.ResumeLayout(false);
            this.splitContainer_Gral.Panel2.ResumeLayout(false);
            this.splitContainer_Gral.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.StatusBar BarraDeStatus;
        private System.Windows.Forms.StatusBarPanel lblModulo;
        private System.Windows.Forms.StatusBarPanel lblServidor;
        private System.Windows.Forms.StatusBarPanel lblBaseDeDatos;
        private System.Windows.Forms.StatusBarPanel lblUsuarioConectado;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnEntrada;
        private System.Windows.Forms.ToolStripButton btnSalida;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Timer tmAutenticacion;
        private System.Windows.Forms.SplitContainer splitContainer_Gral;
        private System.Windows.Forms.Label lblTitulo_Checador;
        private System.Windows.Forms.Label lblProcesoIntegracion;
        private System.Windows.Forms.Timer tmSincronizacion;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnDescargarHuellas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEnviarRegistroChecador;
        private System.Windows.Forms.Label label1;
	}
}