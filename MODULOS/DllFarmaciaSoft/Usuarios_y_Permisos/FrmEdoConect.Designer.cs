namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    partial class FrmEdoConect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEdoConect));
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblMensaje = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.Frame = new System.Windows.Forms.GroupBox();
            this.lblCancelarConexion = new System.Windows.Forms.Label();
            this.btnCancelarConexion = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Frame.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.SteelBlue;
            this.pgBar.Location = new System.Drawing.Point(11, 52);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(364, 52);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 2;
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.ForeColor = System.Drawing.Color.Green;
            this.lblMensaje.Location = new System.Drawing.Point(19, 17);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(346, 28);
            this.lblMensaje.TabIndex = 3;
            this.lblMensaje.Text = "Accesando";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer2
            // 
            this.timer2.Interval = 1750;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Frame
            // 
            this.Frame.Controls.Add(this.lblCancelarConexion);
            this.Frame.Controls.Add(this.btnCancelarConexion);
            this.Frame.Controls.Add(this.lblMensaje);
            this.Frame.Controls.Add(this.pgBar);
            this.Frame.Location = new System.Drawing.Point(7, 5);
            this.Frame.Name = "Frame";
            this.Frame.Size = new System.Drawing.Size(388, 171);
            this.Frame.TabIndex = 4;
            this.Frame.TabStop = false;
            // 
            // lblCancelarConexion
            // 
            this.lblCancelarConexion.Image = ((System.Drawing.Image)(resources.GetObject("lblCancelarConexion.Image")));
            this.lblCancelarConexion.Location = new System.Drawing.Point(163, 112);
            this.lblCancelarConexion.Name = "lblCancelarConexion";
            this.lblCancelarConexion.Size = new System.Drawing.Size(45, 45);
            this.lblCancelarConexion.TabIndex = 5;
            this.lblCancelarConexion.Click += new System.EventHandler(this.lblCancelarConexion_Click);
            this.lblCancelarConexion.MouseLeave += new System.EventHandler(this.lblCancelarConexion_MouseLeave);
            this.lblCancelarConexion.MouseHover += new System.EventHandler(this.lblCancelarConexion_MouseHover);
            // 
            // btnCancelarConexion
            // 
            this.btnCancelarConexion.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelarConexion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancelarConexion.BackgroundImage")));
            this.btnCancelarConexion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelarConexion.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancelarConexion.FlatAppearance.BorderSize = 0;
            this.btnCancelarConexion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCancelarConexion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarConexion.Location = new System.Drawing.Point(63, 109);
            this.btnCancelarConexion.Name = "btnCancelarConexion";
            this.btnCancelarConexion.Size = new System.Drawing.Size(22, 24);
            this.btnCancelarConexion.TabIndex = 4;
            this.btnCancelarConexion.UseVisualStyleBackColor = false;
            this.btnCancelarConexion.Visible = false;
            this.btnCancelarConexion.Click += new System.EventHandler(this.btnCancelarConexion_Click);
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // FrmEdoConect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 184);
            this.ControlBox = false;
            this.Controls.Add(this.Frame);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmEdoConect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Acceso";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmEdoConect_FormClosing);
            this.Load += new System.EventHandler(this.FrmEdoConect_Load);
            this.Frame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.GroupBox Frame;
        private System.Windows.Forms.Button btnCancelarConexion;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblCancelarConexion;

    }
}