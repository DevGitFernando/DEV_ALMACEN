namespace Almacen.Catalogos
{
    partial class FrmHuellasPersonal
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNombre = new SC_ControlsCS.scLabelExt();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstvHuellas = new System.Windows.Forms.ListView();
            this.colReferencia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMano = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDedo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRegistrado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFechaRegistro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuHuellas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnCapturarHuella = new System.Windows.Forms.ToolStripMenuItem();
            this.verificarHuellaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBorrarHuella = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuHuellas.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNombre);
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(727, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Usuario Operativo";
            // 
            // lblNombre
            // 
            this.lblNombre.Location = new System.Drawing.Point(13, 20);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.MostrarToolTip = false;
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(697, 28);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstvHuellas);
            this.groupBox2.Location = new System.Drawing.Point(9, 66);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(727, 331);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Captura Huellas Digitales";
            // 
            // lstvHuellas
            // 
            this.lstvHuellas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colReferencia,
            this.colMano,
            this.colDedo,
            this.colRegistrado,
            this.colFechaRegistro,
            this.colStatus});
            this.lstvHuellas.ContextMenuStrip = this.menuHuellas;
            this.lstvHuellas.HideSelection = false;
            this.lstvHuellas.Location = new System.Drawing.Point(13, 20);
            this.lstvHuellas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstvHuellas.Name = "lstvHuellas";
            this.lstvHuellas.Size = new System.Drawing.Size(696, 298);
            this.lstvHuellas.TabIndex = 0;
            this.lstvHuellas.UseCompatibleStateImageBehavior = false;
            this.lstvHuellas.View = System.Windows.Forms.View.Details;
            // 
            // colReferencia
            // 
            this.colReferencia.Text = "Referencia";
            this.colReferencia.Width = 80;
            // 
            // colMano
            // 
            this.colMano.Text = "Mano";
            this.colMano.Width = 90;
            // 
            // colDedo
            // 
            this.colDedo.Text = "Dedo";
            this.colDedo.Width = 90;
            // 
            // colRegistrado
            // 
            this.colRegistrado.Text = "Registrado";
            this.colRegistrado.Width = 80;
            // 
            // colFechaRegistro
            // 
            this.colFechaRegistro.Text = "Fecha";
            this.colFechaRegistro.Width = 96;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 82;
            // 
            // menuHuellas
            // 
            this.menuHuellas.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuHuellas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCapturarHuella,
            this.verificarHuellaToolStripMenuItem,
            this.btnBorrarHuella});
            this.menuHuellas.Name = "menuHuellas";
            this.menuHuellas.Size = new System.Drawing.Size(180, 76);
            // 
            // btnCapturarHuella
            // 
            this.btnCapturarHuella.Name = "btnCapturarHuella";
            this.btnCapturarHuella.Size = new System.Drawing.Size(179, 24);
            this.btnCapturarHuella.Text = "Capturar huella";
            this.btnCapturarHuella.Click += new System.EventHandler(this.btnCapturarHuella_Click);
            // 
            // verificarHuellaToolStripMenuItem
            // 
            this.verificarHuellaToolStripMenuItem.Name = "verificarHuellaToolStripMenuItem";
            this.verificarHuellaToolStripMenuItem.Size = new System.Drawing.Size(179, 24);
            this.verificarHuellaToolStripMenuItem.Text = "Verificar huella";
            this.verificarHuellaToolStripMenuItem.Click += new System.EventHandler(this.verificarHuellaToolStripMenuItem_Click);
            // 
            // btnBorrarHuella
            // 
            this.btnBorrarHuella.Name = "btnBorrarHuella";
            this.btnBorrarHuella.Size = new System.Drawing.Size(179, 24);
            this.btnBorrarHuella.Text = "Borrar huella";
            this.btnBorrarHuella.Click += new System.EventHandler(this.btnBorrarHuella_Click);
            // 
            // FrmHuellasPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 404);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmHuellasPersonal";
            this.ShowIcon = false;
            this.Text = "Huellas Digitales Usuarios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmHuellasPersonal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.menuHuellas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstvHuellas;
        private System.Windows.Forms.ContextMenuStrip menuHuellas;
        private SC_ControlsCS.scLabelExt lblNombre;
        private System.Windows.Forms.ToolStripMenuItem btnCapturarHuella;
        private System.Windows.Forms.ToolStripMenuItem btnBorrarHuella;
        private System.Windows.Forms.ColumnHeader colReferencia;
        private System.Windows.Forms.ColumnHeader colMano;
        private System.Windows.Forms.ColumnHeader colDedo;
        private System.Windows.Forms.ColumnHeader colRegistrado;
        private System.Windows.Forms.ColumnHeader colFechaRegistro;
        private System.Windows.Forms.ToolStripMenuItem verificarHuellaToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader colStatus;
    }
}