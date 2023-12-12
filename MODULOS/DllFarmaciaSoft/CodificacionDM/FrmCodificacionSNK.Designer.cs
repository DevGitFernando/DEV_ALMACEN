namespace DllFarmaciaSoft
{
    partial class FrmCodificacionSNK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCodificacionSNK));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameCodigo = new System.Windows.Forms.GroupBox();
            this.txtCodigo = new SC_ControlsCS.scTextBoxExt();
            this.FrameInformacion = new System.Windows.Forms.GroupBox();
            this.lstResultadoCodificacion = new System.Windows.Forms.ListView();
            this.colUUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colResultado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameCodigo.SuspendLayout();
            this.FrameInformacion.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(925, 25);
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
            this.btnNuevo.Text = "&Limpiar pantalla (CTRL + N)";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // FrameCodigo
            // 
            this.FrameCodigo.Controls.Add(this.txtCodigo);
            this.FrameCodigo.Location = new System.Drawing.Point(12, 28);
            this.FrameCodigo.Name = "FrameCodigo";
            this.FrameCodigo.Size = new System.Drawing.Size(903, 74);
            this.FrameCodigo.TabIndex = 9;
            this.FrameCodigo.TabStop = false;
            this.FrameCodigo.Text = "Código";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigo.Decimales = 2;
            this.txtCodigo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.ForeColor = System.Drawing.Color.Black;
            this.txtCodigo.Location = new System.Drawing.Point(11, 17);
            this.txtCodigo.MaxLength = 100;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PermitirApostrofo = false;
            this.txtCodigo.PermitirNegativos = false;
            this.txtCodigo.Size = new System.Drawing.Size(883, 47);
            this.txtCodigo.TabIndex = 3;
            this.txtCodigo.Text = "00209000201007503001007663001|";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigo_Validating);
            // 
            // FrameInformacion
            // 
            this.FrameInformacion.Controls.Add(this.lstResultadoCodificacion);
            this.FrameInformacion.Location = new System.Drawing.Point(12, 105);
            this.FrameInformacion.Name = "FrameInformacion";
            this.FrameInformacion.Size = new System.Drawing.Size(904, 306);
            this.FrameInformacion.TabIndex = 11;
            this.FrameInformacion.TabStop = false;
            this.FrameInformacion.Text = "Información";
            // 
            // lstResultadoCodificacion
            // 
            this.lstResultadoCodificacion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colUUID,
            this.colResultado});
            this.lstResultadoCodificacion.FullRowSelect = true;
            this.lstResultadoCodificacion.Location = new System.Drawing.Point(10, 16);
            this.lstResultadoCodificacion.MultiSelect = false;
            this.lstResultadoCodificacion.Name = "lstResultadoCodificacion";
            this.lstResultadoCodificacion.ShowItemToolTips = true;
            this.lstResultadoCodificacion.Size = new System.Drawing.Size(884, 281);
            this.lstResultadoCodificacion.TabIndex = 0;
            this.lstResultadoCodificacion.UseCompatibleStateImageBehavior = false;
            this.lstResultadoCodificacion.View = System.Windows.Forms.View.Details;
            // 
            // colUUID
            // 
            this.colUUID.Text = "UUID";
            this.colUUID.Width = 600;
            // 
            // colResultado
            // 
            this.colResultado.Text = "Resultado";
            this.colResultado.Width = 250;
            // 
            // FrmCodificacionSNK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 421);
            this.Controls.Add(this.FrameInformacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameCodigo);
            this.Name = "FrmCodificacionSNK";
            this.Text = "Codificación de productos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCodificacionSNK_FormClosing);
            this.Load += new System.EventHandler(this.FrmCodificacionSNK_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameCodigo.ResumeLayout(false);
            this.FrameCodigo.PerformLayout();
            this.FrameInformacion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.GroupBox FrameCodigo;
        private SC_ControlsCS.scTextBoxExt txtCodigo;
        private System.Windows.Forms.GroupBox FrameInformacion;
        private System.Windows.Forms.ListView lstResultadoCodificacion;
        private System.Windows.Forms.ColumnHeader colUUID;
        private System.Windows.Forms.ColumnHeader colResultado;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}