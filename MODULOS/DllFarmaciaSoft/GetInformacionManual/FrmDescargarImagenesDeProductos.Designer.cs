namespace DllFarmaciaSoft.GetInformacionManual
{
    partial class FrmDescargarImagenesDeProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDescargarImagenesDeProductos));
            this.FrameOrigenDatos = new System.Windows.Forms.GroupBox();
            this.chkDescargarTodo = new System.Windows.Forms.CheckBox();
            this.txtHasta = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDesde = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnObtenerImagenes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStriplblResultado = new System.Windows.Forms.ToolStripLabel();
            this.lblProceso = new System.Windows.Forms.Label();
            this.FrameOrigenDatos.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameOrigenDatos
            // 
            this.FrameOrigenDatos.Controls.Add(this.chkDescargarTodo);
            this.FrameOrigenDatos.Controls.Add(this.txtHasta);
            this.FrameOrigenDatos.Controls.Add(this.label2);
            this.FrameOrigenDatos.Controls.Add(this.txtDesde);
            this.FrameOrigenDatos.Controls.Add(this.label1);
            this.FrameOrigenDatos.Location = new System.Drawing.Point(12, 28);
            this.FrameOrigenDatos.Name = "FrameOrigenDatos";
            this.FrameOrigenDatos.Size = new System.Drawing.Size(545, 52);
            this.FrameOrigenDatos.TabIndex = 1;
            this.FrameOrigenDatos.TabStop = false;
            this.FrameOrigenDatos.Text = "Rango de Productos";
            // 
            // chkDescargarTodo
            // 
            this.chkDescargarTodo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDescargarTodo.Location = new System.Drawing.Point(377, 17);
            this.chkDescargarTodo.Name = "chkDescargarTodo";
            this.chkDescargarTodo.Size = new System.Drawing.Size(154, 24);
            this.chkDescargarTodo.TabIndex = 2;
            this.chkDescargarTodo.Text = "Catálogo completo";
            this.chkDescargarTodo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDescargarTodo.UseVisualStyleBackColor = true;
            // 
            // txtHasta
            // 
            this.txtHasta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHasta.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtHasta.Decimales = 2;
            this.txtHasta.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtHasta.ForeColor = System.Drawing.Color.Black;
            this.txtHasta.Location = new System.Drawing.Point(239, 19);
            this.txtHasta.MaxLength = 8;
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.PermitirApostrofo = false;
            this.txtHasta.PermitirNegativos = false;
            this.txtHasta.Size = new System.Drawing.Size(100, 20);
            this.txtHasta.TabIndex = 1;
            this.txtHasta.Text = "01234567";
            this.txtHasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(189, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hasta :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDesde
            // 
            this.txtDesde.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDesde.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDesde.Decimales = 2;
            this.txtDesde.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDesde.ForeColor = System.Drawing.Color.Black;
            this.txtDesde.Location = new System.Drawing.Point(70, 19);
            this.txtDesde.MaxLength = 8;
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.PermitirApostrofo = false;
            this.txtDesde.PermitirNegativos = false;
            this.txtDesde.Size = new System.Drawing.Size(100, 20);
            this.txtDesde.TabIndex = 0;
            this.txtDesde.Text = "01234567";
            this.txtDesde.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Desde :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnObtenerImagenes,
            this.toolStripSeparator2,
            this.toolStriplblResultado});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(566, 25);
            this.toolStripBarraMenu.TabIndex = 0;
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
            // btnObtenerImagenes
            // 
            this.btnObtenerImagenes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnObtenerImagenes.Image = ((System.Drawing.Image)(resources.GetObject("btnObtenerImagenes.Image")));
            this.btnObtenerImagenes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnObtenerImagenes.Name = "btnObtenerImagenes";
            this.btnObtenerImagenes.Size = new System.Drawing.Size(23, 22);
            this.btnObtenerImagenes.Text = "Descargar información";
            this.btnObtenerImagenes.Click += new System.EventHandler(this.btnObtenerImagenes_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStriplblResultado
            // 
            this.toolStriplblResultado.Name = "toolStriplblResultado";
            this.toolStriplblResultado.Size = new System.Drawing.Size(0, 22);
            // 
            // lblProceso
            // 
            this.lblProceso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProceso.Location = new System.Drawing.Point(140, 4);
            this.lblProceso.Name = "lblProceso";
            this.lblProceso.Size = new System.Drawing.Size(414, 18);
            this.lblProceso.TabIndex = 2;
            this.lblProceso.Text = "label3";
            this.lblProceso.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProceso.EnabledChanged += new System.EventHandler(this.lblProceso_EnabledChanged);
            // 
            // FrmDescargarImagenesDeProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 91);
            this.Controls.Add(this.lblProceso);
            this.Controls.Add(this.FrameOrigenDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmDescargarImagenesDeProductos";
            this.Text = "Descarga de Imagenes de Productos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmDescargarImagenesDeProductos_Load);
            this.FrameOrigenDatos.ResumeLayout(false);
            this.FrameOrigenDatos.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameOrigenDatos;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnObtenerImagenes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStriplblResultado;
        private SC_ControlsCS.scTextBoxExt txtDesde;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDescargarTodo;
        private System.Windows.Forms.Label lblProceso;
    }
}