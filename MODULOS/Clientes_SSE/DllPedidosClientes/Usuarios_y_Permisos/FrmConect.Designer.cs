namespace DllPedidosClientes.Usuarios_y_Permisos
{
    partial class FrmConect
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConect));
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lbl = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCancelarConexion = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(6, 20);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(320, 34);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 2;
            // 
            // lbl
            // 
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(13, 55);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(300, 18);
            this.lbl.TabIndex = 3;
            this.lbl.Text = "Estableciendo conexión con el servidor";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer2
            // 
            this.timer2.Interval = 1750;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCancelarConexion);
            this.groupBox1.Controls.Add(this.lbl);
            this.groupBox1.Controls.Add(this.pgBar);
            this.groupBox1.Location = new System.Drawing.Point(7, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 92);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // lblCancelarConexion
            // 
            this.lblCancelarConexion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCancelarConexion.Image = ((System.Drawing.Image)(resources.GetObject("lblCancelarConexion.Image")));
            this.lblCancelarConexion.Location = new System.Drawing.Point(306, 68);
            this.lblCancelarConexion.Name = "lblCancelarConexion";
            this.lblCancelarConexion.Size = new System.Drawing.Size(20, 20);
            this.lblCancelarConexion.TabIndex = 6;
            this.lblCancelarConexion.Click += new System.EventHandler(this.lblCancelarConexion_Click);
            this.lblCancelarConexion.MouseLeave += new System.EventHandler(this.lblCancelarConexion_MouseLeave);
            this.lblCancelarConexion.MouseHover += new System.EventHandler(this.lblCancelarConexion_MouseHover);
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // FrmConect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 102);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmConect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Conectando con el servidor";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEdoConect_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCancelarConexion;
        private System.Windows.Forms.ToolTip toolTip;

    }
}