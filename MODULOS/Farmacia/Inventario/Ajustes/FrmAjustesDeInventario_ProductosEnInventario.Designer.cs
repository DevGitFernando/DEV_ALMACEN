namespace Farmacia.Inventario
{
    partial class FrmAjustesDeInventario_ProductosEnInventario
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
            this.lblMensajes = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstvProductos = new System.Windows.Forms.ListView();
            this.colPoliza = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProducto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCodigoEAN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 365);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(764, 24);
            this.lblMensajes.TabIndex = 11;
            this.lblMensajes.Text = "<F12> Cerrar pantalla";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstvProductos);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 354);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Productos";
            // 
            // lstvProductos
            // 
            this.lstvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvProductos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPoliza,
            this.colProducto,
            this.colCodigoEAN,
            this.colDescripcion});
            this.lstvProductos.Location = new System.Drawing.Point(10, 16);
            this.lstvProductos.Name = "lstvProductos";
            this.lstvProductos.Size = new System.Drawing.Size(731, 330);
            this.lstvProductos.TabIndex = 0;
            this.lstvProductos.UseCompatibleStateImageBehavior = false;
            this.lstvProductos.View = System.Windows.Forms.View.Details;
            // 
            // colPoliza
            // 
            this.colPoliza.Text = "Póliza";
            this.colPoliza.Width = 80;
            // 
            // colProducto
            // 
            this.colProducto.Text = "Id Producto";
            this.colProducto.Width = 80;
            // 
            // colCodigoEAN
            // 
            this.colCodigoEAN.Text = "Código EAN";
            this.colCodigoEAN.Width = 100;
            // 
            // colDescripcion
            // 
            this.colDescripcion.Text = "Descripción";
            this.colDescripcion.Width = 440;
            // 
            // FrmAjustesDeInventario_ProductosEnInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 389);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblMensajes);
            this.Name = "FrmAjustesDeInventario_ProductosEnInventario";
            this.Text = "Productos Marcados en Inventario Físico";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmAjustesDeInventario_ProductosEnInventario_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAjustesDeInventario_ProductosEnInventario_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstvProductos;
        private System.Windows.Forms.ColumnHeader colPoliza;
        private System.Windows.Forms.ColumnHeader colProducto;
        private System.Windows.Forms.ColumnHeader colCodigoEAN;
        private System.Windows.Forms.ColumnHeader colDescripcion;
    }
}