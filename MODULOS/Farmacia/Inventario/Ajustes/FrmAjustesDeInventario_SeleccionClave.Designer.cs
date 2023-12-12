namespace Farmacia.Inventario
{
    partial class FrmAjustesDeInventario_SeleccionClave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAjustesDeInventario_SeleccionClave));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAgregarClave = new System.Windows.Forms.Button();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtClave = new SC_ControlsCS.scTextBoxExt();
            this.txtCambio = new SC_ControlsCS.scTextBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.listClaves = new System.Windows.Forms.ListView();
            this.colClaveSSA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuOpciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnEliminarClave = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameClaves.SuspendLayout();
            this.menuOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnAgregarClave);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.txtClave);
            this.groupBox1.Controls.Add(this.txtCambio);
            this.groupBox1.Location = new System.Drawing.Point(9, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(754, 91);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Clave SSA";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(21, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "Observación :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(69, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "Clave SSA :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAgregarClave
            // 
            this.btnAgregarClave.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarClave.Image")));
            this.btnAgregarClave.Location = new System.Drawing.Point(278, 18);
            this.btnAgregarClave.Name = "btnAgregarClave";
            this.btnAgregarClave.Size = new System.Drawing.Size(34, 23);
            this.btnAgregarClave.TabIndex = 1;
            this.btnAgregarClave.UseVisualStyleBackColor = true;
            this.btnAgregarClave.Click += new System.EventHandler(this.btnAgregarClave_Click);
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(115, 42);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(629, 40);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "label2";
            // 
            // txtClave
            // 
            this.txtClave.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClave.Decimales = 2;
            this.txtClave.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClave.ForeColor = System.Drawing.Color.Black;
            this.txtClave.Location = new System.Drawing.Point(115, 19);
            this.txtClave.MaxLength = 30;
            this.txtClave.Name = "txtClave";
            this.txtClave.PermitirApostrofo = false;
            this.txtClave.PermitirNegativos = false;
            this.txtClave.Size = new System.Drawing.Size(157, 20);
            this.txtClave.TabIndex = 0;
            this.txtClave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClave.TextChanged += new System.EventHandler(this.txtClave_TextChanged);
            this.txtClave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClave_KeyDown);
            this.txtClave.Validating += new System.ComponentModel.CancelEventHandler(this.txtClave_Validating);
            // 
            // txtCambio
            // 
            this.txtCambio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCambio.Decimales = 2;
            this.txtCambio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCambio.ForeColor = System.Drawing.Color.Black;
            this.txtCambio.Location = new System.Drawing.Point(407, 18);
            this.txtCambio.MaxLength = 30;
            this.txtCambio.Name = "txtCambio";
            this.txtCambio.PermitirApostrofo = false;
            this.txtCambio.PermitirNegativos = false;
            this.txtCambio.Size = new System.Drawing.Size(34, 20);
            this.txtCambio.TabIndex = 2;
            this.txtCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(770, 25);
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
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
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
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameClaves
            // 
            this.FrameClaves.Controls.Add(this.listClaves);
            this.FrameClaves.Location = new System.Drawing.Point(9, 121);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(754, 241);
            this.FrameClaves.TabIndex = 3;
            this.FrameClaves.TabStop = false;
            // 
            // listClaves
            // 
            this.listClaves.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colClaveSSA,
            this.colDescripcion});
            this.listClaves.ContextMenuStrip = this.menuOpciones;
            this.listClaves.Location = new System.Drawing.Point(8, 14);
            this.listClaves.Name = "listClaves";
            this.listClaves.Size = new System.Drawing.Size(736, 219);
            this.listClaves.TabIndex = 0;
            this.listClaves.UseCompatibleStateImageBehavior = false;
            this.listClaves.View = System.Windows.Forms.View.Details;
            this.listClaves.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listClaves_KeyDown);
            // 
            // colClaveSSA
            // 
            this.colClaveSSA.Text = "Clave SSA";
            this.colClaveSSA.Width = 100;
            // 
            // colDescripcion
            // 
            this.colDescripcion.Text = "Descripción";
            this.colDescripcion.Width = 600;
            // 
            // menuOpciones
            // 
            this.menuOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEliminarClave});
            this.menuOpciones.Name = "menuOpciones";
            this.menuOpciones.Size = new System.Drawing.Size(187, 26);
            // 
            // btnEliminarClave
            // 
            this.btnEliminarClave.Name = "btnEliminarClave";
            this.btnEliminarClave.Size = new System.Drawing.Size(186, 22);
            this.btnEliminarClave.Text = "[SUPR] Eliminar clave";
            this.btnEliminarClave.Click += new System.EventHandler(this.btnEliminarClave_Click);
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 366);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(770, 24);
            this.lblMensajes.TabIndex = 12;
            this.lblMensajes.Text = "Clic derecho ver opciones";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmAjustesDeInventario_SeleccionClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 390);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.FrameClaves);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmAjustesDeInventario_SeleccionClave";
            this.Text = "Captura de Claves para Inventario Físico";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmAjustesDeInventario_SeleccionClave_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameClaves.ResumeLayout(false);
            this.menuOpciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDescripcion;
        private SC_ControlsCS.scTextBoxExt txtClave;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private SC_ControlsCS.scTextBoxExt txtCambio;
        private System.Windows.Forms.Button btnAgregarClave;
        private System.Windows.Forms.GroupBox FrameClaves;
        private System.Windows.Forms.ListView listClaves;
        private System.Windows.Forms.ColumnHeader colClaveSSA;
        private System.Windows.Forms.ColumnHeader colDescripcion;
        private System.Windows.Forms.ContextMenuStrip menuOpciones;
        private System.Windows.Forms.ToolStripMenuItem btnEliminarClave;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
    }
}