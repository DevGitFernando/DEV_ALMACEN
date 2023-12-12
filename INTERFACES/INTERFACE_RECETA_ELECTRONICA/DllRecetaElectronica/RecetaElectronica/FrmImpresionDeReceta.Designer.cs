namespace DllRecetaElectronica.ECE
{
    partial class FrmImpresionDeReceta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImpresionDeReceta));
            this.lblFolio = new System.Windows.Forms.Label();
            this.txtReceta = new SC_ControlsCS.scTextBoxExt();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.txtMaterno = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.txtApPaterno = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.listviewRecetas = new System.Windows.Forms.ListView();
            this.colSecuencial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolioReceta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFechaReceta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombreBeneficiario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombreMedico = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.FrameDatos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFolio
            // 
            this.lblFolio.Location = new System.Drawing.Point(63, 24);
            this.lblFolio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(61, 17);
            this.lblFolio.TabIndex = 3;
            this.lblFolio.Text = "Receta :";
            this.lblFolio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceta
            // 
            this.txtReceta.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReceta.Decimales = 2;
            this.txtReceta.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtReceta.ForeColor = System.Drawing.Color.Black;
            this.txtReceta.Location = new System.Drawing.Point(129, 23);
            this.txtReceta.Margin = new System.Windows.Forms.Padding(4);
            this.txtReceta.MaxLength = 8;
            this.txtReceta.Name = "txtReceta";
            this.txtReceta.PermitirApostrofo = false;
            this.txtReceta.PermitirNegativos = false;
            this.txtReceta.Size = new System.Drawing.Size(199, 22);
            this.txtReceta.TabIndex = 0;
            this.txtReceta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.txtMaterno);
            this.FrameDatos.Controls.Add(this.label3);
            this.FrameDatos.Controls.Add(this.txtApPaterno);
            this.FrameDatos.Controls.Add(this.label2);
            this.FrameDatos.Controls.Add(this.txtNombre);
            this.FrameDatos.Controls.Add(this.label1);
            this.FrameDatos.Controls.Add(this.txtReceta);
            this.FrameDatos.Controls.Add(this.lblFolio);
            this.FrameDatos.Location = new System.Drawing.Point(10, 29);
            this.FrameDatos.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDatos.Size = new System.Drawing.Size(1155, 108);
            this.FrameDatos.TabIndex = 3;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Datos filtro";
            // 
            // txtMaterno
            // 
            this.txtMaterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtMaterno.Decimales = 2;
            this.txtMaterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtMaterno.ForeColor = System.Drawing.Color.Black;
            this.txtMaterno.Location = new System.Drawing.Point(860, 54);
            this.txtMaterno.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaterno.MaxLength = 8;
            this.txtMaterno.Name = "txtMaterno";
            this.txtMaterno.PermitirApostrofo = false;
            this.txtMaterno.PermitirNegativos = false;
            this.txtMaterno.Size = new System.Drawing.Size(199, 22);
            this.txtMaterno.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(714, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Ap Materno :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtApPaterno
            // 
            this.txtApPaterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtApPaterno.Decimales = 2;
            this.txtApPaterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtApPaterno.ForeColor = System.Drawing.Color.Black;
            this.txtApPaterno.Location = new System.Drawing.Point(487, 54);
            this.txtApPaterno.Margin = new System.Windows.Forms.Padding(4);
            this.txtApPaterno.MaxLength = 8;
            this.txtApPaterno.Name = "txtApPaterno";
            this.txtApPaterno.PermitirApostrofo = false;
            this.txtApPaterno.PermitirNegativos = false;
            this.txtApPaterno.Size = new System.Drawing.Size(199, 22);
            this.txtApPaterno.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(341, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Ap Paterno :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNombre
            // 
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 2;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(129, 54);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombre.MaxLength = 8;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(199, 22);
            this.txtNombre.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(27, 57);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nombre :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMensajes);
            this.groupBox1.Controls.Add(this.listviewRecetas);
            this.groupBox1.Location = new System.Drawing.Point(10, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1159, 563);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(3, 530);
            this.lblMensajes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1153, 30);
            this.lblMensajes.TabIndex = 11;
            this.lblMensajes.Text = "Doble clic sobre el renglón para Imprimir";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listviewRecetas
            // 
            this.listviewRecetas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSecuencial,
            this.colFolioReceta,
            this.colFechaReceta,
            this.colNombreBeneficiario,
            this.colNombreMedico});
            this.listviewRecetas.Location = new System.Drawing.Point(10, 16);
            this.listviewRecetas.Name = "listviewRecetas";
            this.listviewRecetas.Size = new System.Drawing.Size(1138, 509);
            this.listviewRecetas.TabIndex = 0;
            this.listviewRecetas.UseCompatibleStateImageBehavior = false;
            this.listviewRecetas.View = System.Windows.Forms.View.Details;
            this.listviewRecetas.DoubleClick += new System.EventHandler(this.listviewRecetas_DoubleClick);
            // 
            // colSecuencial
            // 
            this.colSecuencial.Text = "Secuencial";
            this.colSecuencial.Width = 120;
            // 
            // colFolioReceta
            // 
            this.colFolioReceta.Text = "Folio de receta";
            this.colFolioReceta.Width = 150;
            // 
            // colFechaReceta
            // 
            this.colFechaReceta.Text = "Fecha de receta";
            this.colFechaReceta.Width = 150;
            // 
            // colNombreBeneficiario
            // 
            this.colNombreBeneficiario.Text = "Beneficiario";
            this.colNombreBeneficiario.Width = 340;
            // 
            // colNombreMedico
            // 
            this.colNombreMedico.Text = "Nombre médico preescribe";
            this.colNombreMedico.Width = 340;
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
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1178, 25);
            this.toolStripBarraMenu.TabIndex = 2;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // FrmImpresionDeReceta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 710);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmImpresionDeReceta";
            this.Text = "Impresión de receta";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmImpresionDeReceta_Load);
            this.FrameDatos.ResumeLayout(false);
            this.FrameDatos.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        private System.Windows.Forms.Label lblFolio;
        private SC_ControlsCS.scTextBoxExt txtReceta;
        private System.Windows.Forms.GroupBox FrameDatos;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMensajes;
        private SC_ControlsCS.scTextBoxExt txtMaterno;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtApPaterno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listviewRecetas;
        private System.Windows.Forms.ColumnHeader colSecuencial;
        private System.Windows.Forms.ColumnHeader colFolioReceta;
        private System.Windows.Forms.ColumnHeader colFechaReceta;
        private System.Windows.Forms.ColumnHeader colNombreBeneficiario;
        private System.Windows.Forms.ColumnHeader colNombreMedico;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
       
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;

    }
}