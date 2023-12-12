namespace DllRecetaElectronica.ECE
{
    partial class FrmRecetasElectronicas_Claves
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listviewRecetas = new System.Windows.Forms.ListView();
            this.colEnCuadroBasico = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClaveSSA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCantidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescripcionClave = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.listviewRecetas);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(970, 228);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // listviewRecetas
            // 
            this.listviewRecetas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listviewRecetas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEnCuadroBasico,
            this.colClaveSSA,
            this.colCantidad,
            this.colDescripcionClave});
            this.listviewRecetas.Location = new System.Drawing.Point(10, 16);
            this.listviewRecetas.Name = "listviewRecetas";
            this.listviewRecetas.Size = new System.Drawing.Size(944, 197);
            this.listviewRecetas.TabIndex = 0;
            this.listviewRecetas.UseCompatibleStateImageBehavior = false;
            this.listviewRecetas.View = System.Windows.Forms.View.Details;
            // 
            // colEnCuadroBasico
            // 
            this.colEnCuadroBasico.Text = "En cuadro básico";
            this.colEnCuadroBasico.Width = 120;
            // 
            // colClaveSSA
            // 
            this.colClaveSSA.Text = "Código";
            this.colClaveSSA.Width = 150;
            // 
            // colCantidad
            // 
            this.colCantidad.Text = "Cantidad en piezas";
            this.colCantidad.Width = 150;
            // 
            // colDescripcionClave
            // 
            this.colDescripcionClave.Text = "Descripción";
            this.colDescripcionClave.Width = 480;
            // 
            // FrmRecetasElectronicas_Claves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 252);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmRecetasElectronicas_Claves";
            this.Text = "Claves solicitadas en receta electrónica";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRecetasElectronicas_Claves_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listviewRecetas;
        private System.Windows.Forms.ColumnHeader colClaveSSA;
        private System.Windows.Forms.ColumnHeader colCantidad;
        private System.Windows.Forms.ColumnHeader colDescripcionClave;
        private System.Windows.Forms.ColumnHeader colEnCuadroBasico;
    }
}