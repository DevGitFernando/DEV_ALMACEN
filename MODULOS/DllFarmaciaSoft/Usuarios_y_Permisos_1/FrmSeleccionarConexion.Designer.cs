namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    partial class FrmSeleccionarConexion
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
            this.panel = new System.Windows.Forms.Panel();
            this.FrameConfiguraciones = new System.Windows.Forms.GroupBox();
            this.cboNodos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.FrameConfiguraciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel.AutoScroll = true;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Location = new System.Drawing.Point(10, 62);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(562, 151);
            this.panel.TabIndex = 4;
            // 
            // FrameConfiguraciones
            // 
            this.FrameConfiguraciones.Controls.Add(this.cboNodos);
            this.FrameConfiguraciones.Controls.Add(this.label2);
            this.FrameConfiguraciones.Location = new System.Drawing.Point(10, 11);
            this.FrameConfiguraciones.Name = "FrameConfiguraciones";
            this.FrameConfiguraciones.Size = new System.Drawing.Size(562, 47);
            this.FrameConfiguraciones.TabIndex = 3;
            this.FrameConfiguraciones.TabStop = false;
            this.FrameConfiguraciones.Text = "Conexiones";
            // 
            // cboNodos
            // 
            this.cboNodos.DropDownHeight = 240;
            this.cboNodos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNodos.FormattingEnabled = true;
            this.cboNodos.IntegralHeight = false;
            this.cboNodos.Location = new System.Drawing.Point(82, 19);
            this.cboNodos.MaxDropDownItems = 20;
            this.cboNodos.Name = "cboNodos";
            this.cboNodos.Size = new System.Drawing.Size(471, 21);
            this.cboNodos.TabIndex = 2;
            this.cboNodos.SelectedIndexChanged += new System.EventHandler(this.cboNodos_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Nodos :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(472, 219);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 24);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cerrar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.Location = new System.Drawing.Point(368, 219);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(100, 24);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FrmSeleccionarConexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 251);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.FrameConfiguraciones);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmSeleccionarConexion";
            this.Text = "Seleccionar conexion de sistema";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmSeleccionarConexion_Load);
            this.FrameConfiguraciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.GroupBox FrameConfiguraciones;
        private System.Windows.Forms.ComboBox cboNodos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
    }
}