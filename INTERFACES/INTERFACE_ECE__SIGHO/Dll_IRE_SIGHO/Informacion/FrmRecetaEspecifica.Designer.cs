namespace Dll_IRE_SIGHO.Informacion
{
    partial class FrmRecetaEspecifica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecetaEspecifica));
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReceta = new SC_ControlsCS.scTextBoxExt();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.cboClues = new SC_ControlsCS.scComboBoxExt();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.FrameDatos.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 21);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 20);
            this.label6.TabIndex = 34;
            this.label6.Text = "CLUES :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 36;
            this.label1.Text = "Folio Receta :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceta
            // 
            this.txtReceta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReceta.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReceta.Decimales = 2;
            this.txtReceta.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReceta.ForeColor = System.Drawing.Color.Black;
            this.txtReceta.Location = new System.Drawing.Point(117, 54);
            this.txtReceta.Margin = new System.Windows.Forms.Padding(4);
            this.txtReceta.MaxLength = 100;
            this.txtReceta.Name = "txtReceta";
            this.txtReceta.PermitirApostrofo = false;
            this.txtReceta.PermitirNegativos = false;
            this.txtReceta.Size = new System.Drawing.Size(159, 22);
            this.txtReceta.TabIndex = 35;
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.cboClues);
            this.FrameDatos.Controls.Add(this.txtReceta);
            this.FrameDatos.Controls.Add(this.label1);
            this.FrameDatos.Controls.Add(this.label6);
            this.FrameDatos.Location = new System.Drawing.Point(16, 34);
            this.FrameDatos.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDatos.Size = new System.Drawing.Size(777, 89);
            this.FrameDatos.TabIndex = 2;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Datos Generales";
            // 
            // cboClues
            // 
            this.cboClues.BackColorEnabled = System.Drawing.Color.White;
            this.cboClues.Data = "";
            this.cboClues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClues.Filtro = " 1 = 1";
            this.cboClues.FormattingEnabled = true;
            this.cboClues.ListaItemsBusqueda = 20;
            this.cboClues.Location = new System.Drawing.Point(117, 21);
            this.cboClues.Margin = new System.Windows.Forms.Padding(4);
            this.cboClues.MostrarToolTip = false;
            this.cboClues.Name = "cboClues";
            this.cboClues.Size = new System.Drawing.Size(647, 24);
            this.cboClues.TabIndex = 38;
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
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(806, 25);
            this.toolStripBarraMenu.TabIndex = 1;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // FrmRecetaEspecifica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 131);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmRecetaEspecifica";
            this.Text = "Descargar Receta";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmDesplazamientosEdosLaboratorios_Load);
            this.FrameDatos.ResumeLayout(false);
            this.FrameDatos.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtReceta;
        private System.Windows.Forms.GroupBox FrameDatos;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private SC_ControlsCS.scComboBoxExt cboClues;

    }
}