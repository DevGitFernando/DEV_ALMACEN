namespace DtUtileriasBD
{
    partial class FrmExtraerBD
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
            this.btnProcesar = new System.Windows.Forms.Button();
            this.FramePassword = new System.Windows.Forms.GroupBox();
            this.chkProtegido = new System.Windows.Forms.CheckBox();
            this.txtPassword = new SC_ControlsCS.scTextBoxExt();
            this.FrameOrigen = new System.Windows.Forms.GroupBox();
            this.btnDestino = new System.Windows.Forms.Button();
            this.txtOrigen = new SC_ControlsCS.scTextBoxExt();
            this.FrameDestino = new System.Windows.Forms.GroupBox();
            this.btnOrigen = new System.Windows.Forms.Button();
            this.txtDestino = new SC_ControlsCS.scTextBoxExt();
            this.FramePassword.SuspendLayout();
            this.FrameOrigen.SuspendLayout();
            this.FrameDestino.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(574, 166);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(124, 23);
            this.btnProcesar.TabIndex = 3;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // FramePassword
            // 
            this.FramePassword.Controls.Add(this.chkProtegido);
            this.FramePassword.Controls.Add(this.txtPassword);
            this.FramePassword.Location = new System.Drawing.Point(7, 110);
            this.FramePassword.Name = "FramePassword";
            this.FramePassword.Size = new System.Drawing.Size(702, 50);
            this.FramePassword.TabIndex = 2;
            this.FramePassword.TabStop = false;
            this.FramePassword.Text = "Seguridad";
            // 
            // chkProtegido
            // 
            this.chkProtegido.Location = new System.Drawing.Point(10, 21);
            this.chkProtegido.Name = "chkProtegido";
            this.chkProtegido.Size = new System.Drawing.Size(149, 17);
            this.chkProtegido.TabIndex = 0;
            this.chkProtegido.Text = "Protegido con Contraseña";
            this.chkProtegido.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPassword.Decimales = 2;
            this.txtPassword.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(165, 19);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PermitirApostrofo = false;
            this.txtPassword.PermitirNegativos = false;
            this.txtPassword.Size = new System.Drawing.Size(526, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // FrameOrigen
            // 
            this.FrameOrigen.Controls.Add(this.btnDestino);
            this.FrameOrigen.Controls.Add(this.txtOrigen);
            this.FrameOrigen.Location = new System.Drawing.Point(7, 8);
            this.FrameOrigen.Name = "FrameOrigen";
            this.FrameOrigen.Size = new System.Drawing.Size(702, 50);
            this.FrameOrigen.TabIndex = 0;
            this.FrameOrigen.TabStop = false;
            this.FrameOrigen.Text = "Archivo origen";
            // 
            // btnDestino
            // 
            this.btnDestino.Location = new System.Drawing.Point(660, 16);
            this.btnDestino.Name = "btnDestino";
            this.btnDestino.Size = new System.Drawing.Size(31, 23);
            this.btnDestino.TabIndex = 1;
            this.btnDestino.Text = "...";
            this.btnDestino.UseVisualStyleBackColor = true;
            this.btnDestino.Click += new System.EventHandler(this.btnDestino_Click);
            // 
            // txtOrigen
            // 
            this.txtOrigen.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtOrigen.Decimales = 2;
            this.txtOrigen.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtOrigen.ForeColor = System.Drawing.Color.Black;
            this.txtOrigen.Location = new System.Drawing.Point(10, 19);
            this.txtOrigen.Name = "txtOrigen";
            this.txtOrigen.PermitirApostrofo = false;
            this.txtOrigen.PermitirNegativos = false;
            this.txtOrigen.Size = new System.Drawing.Size(644, 20);
            this.txtOrigen.TabIndex = 0;
            // 
            // FrameDestino
            // 
            this.FrameDestino.Controls.Add(this.btnOrigen);
            this.FrameDestino.Controls.Add(this.txtDestino);
            this.FrameDestino.Location = new System.Drawing.Point(7, 58);
            this.FrameDestino.Name = "FrameDestino";
            this.FrameDestino.Size = new System.Drawing.Size(702, 50);
            this.FrameDestino.TabIndex = 1;
            this.FrameDestino.TabStop = false;
            this.FrameDestino.Text = "Directorio destino";
            // 
            // btnOrigen
            // 
            this.btnOrigen.Location = new System.Drawing.Point(660, 16);
            this.btnOrigen.Name = "btnOrigen";
            this.btnOrigen.Size = new System.Drawing.Size(31, 23);
            this.btnOrigen.TabIndex = 1;
            this.btnOrigen.Text = "...";
            this.btnOrigen.UseVisualStyleBackColor = true;
            this.btnOrigen.Click += new System.EventHandler(this.btnOrigen_Click);
            // 
            // txtDestino
            // 
            this.txtDestino.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDestino.Decimales = 2;
            this.txtDestino.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDestino.ForeColor = System.Drawing.Color.Black;
            this.txtDestino.Location = new System.Drawing.Point(10, 19);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.PermitirApostrofo = false;
            this.txtDestino.PermitirNegativos = false;
            this.txtDestino.Size = new System.Drawing.Size(644, 20);
            this.txtDestino.TabIndex = 0;
            // 
            // FrmExtraerBD
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 197);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.FramePassword);
            this.Controls.Add(this.FrameOrigen);
            this.Controls.Add(this.FrameDestino);
            this.Name = "FrmExtraerBD";
            this.Text = "Extraer Base de Datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmExtraerBD_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmExtraerBD_DragEnter);
            this.FramePassword.ResumeLayout(false);
            this.FramePassword.PerformLayout();
            this.FrameOrigen.ResumeLayout(false);
            this.FrameOrigen.PerformLayout();
            this.FrameDestino.ResumeLayout(false);
            this.FrameDestino.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.GroupBox FramePassword;
        private System.Windows.Forms.CheckBox chkProtegido;
        private SC_ControlsCS.scTextBoxExt txtPassword;
        private System.Windows.Forms.GroupBox FrameOrigen;
        private System.Windows.Forms.Button btnDestino;
        private SC_ControlsCS.scTextBoxExt txtOrigen;
        private System.Windows.Forms.GroupBox FrameDestino;
        private System.Windows.Forms.Button btnOrigen;
        private SC_ControlsCS.scTextBoxExt txtDestino;
    }
}