namespace DllCompras.Informacion
{
    partial class FrmListaClavesReporte
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
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.listClaves = new System.Windows.Forms.ListView();
            this.colClaveSSA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuOpciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnEliminarClave = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.FrameClaves.SuspendLayout();
            this.menuOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameClaves
            // 
            this.FrameClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameClaves.Controls.Add(this.listClaves);
            this.FrameClaves.Location = new System.Drawing.Point(7, 5);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(569, 329);
            this.FrameClaves.TabIndex = 0;
            this.FrameClaves.TabStop = false;
            // 
            // listClaves
            // 
            this.listClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listClaves.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colClaveSSA,
            this.colDescripcion});
            this.listClaves.ContextMenuStrip = this.menuOpciones;
            this.listClaves.Location = new System.Drawing.Point(8, 14);
            this.listClaves.Name = "listClaves";
            this.listClaves.Size = new System.Drawing.Size(554, 307);
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
            this.colDescripcion.Width = 380;
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
            this.lblMensajes.Location = new System.Drawing.Point(0, 337);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(584, 24);
            this.lblMensajes.TabIndex = 11;
            this.lblMensajes.Text = " Clic derecho ver opciones";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmListaClavesReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.FrameClaves);
            this.Name = "FrmListaClavesReporte";
            this.Text = "Lista de claves";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmListaClavesReporte_FormClosing);
            this.Load += new System.EventHandler(this.FrmListaClavesReporte_Load);
            this.FrameClaves.ResumeLayout(false);
            this.menuOpciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameClaves;
        private System.Windows.Forms.ListView listClaves;
        private System.Windows.Forms.ColumnHeader colClaveSSA;
        private System.Windows.Forms.ColumnHeader colDescripcion;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.ContextMenuStrip menuOpciones;
        private System.Windows.Forms.ToolStripMenuItem btnEliminarClave;
    }
}