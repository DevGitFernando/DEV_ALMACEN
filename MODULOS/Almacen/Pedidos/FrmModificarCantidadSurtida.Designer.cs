namespace Almacen.Pedidos
{
    partial class FrmModificarCantidadSurtida
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
            this.txtCantidadNueva = new System.Windows.Forms.NumericUpDown();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.scLabelExt3 = new SC_ControlsCS.scLabelExt();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.lblCantidadAnterior = new SC_ControlsCS.scLabelExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidadNueva)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCantidadNueva);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Controls.Add(this.scLabelExt3);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.scLabelExt2);
            this.groupBox1.Controls.Add(this.lblCantidadAnterior);
            this.groupBox1.Controls.Add(this.scLabelExt1);
            this.groupBox1.Location = new System.Drawing.Point(9, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(600, 164);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // txtCantidadNueva
            // 
            this.txtCantidadNueva.Location = new System.Drawing.Point(169, 63);
            this.txtCantidadNueva.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCantidadNueva.Name = "txtCantidadNueva";
            this.txtCantidadNueva.Size = new System.Drawing.Size(133, 22);
            this.txtCantidadNueva.TabIndex = 7;
            this.txtCantidadNueva.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(464, 126);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(121, 28);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "[F12] Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(340, 126);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(121, 28);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "[F5] Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // scLabelExt3
            // 
            this.scLabelExt3.Location = new System.Drawing.Point(21, 95);
            this.scLabelExt3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt3.MostrarToolTip = false;
            this.scLabelExt3.Name = "scLabelExt3";
            this.scLabelExt3.Size = new System.Drawing.Size(141, 23);
            this.scLabelExt3.TabIndex = 4;
            this.scLabelExt3.Text = "Observaciones : ";
            this.scLabelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(169, 94);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(415, 22);
            this.txtObservaciones.TabIndex = 1;
            this.txtObservaciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(21, 63);
            this.scLabelExt2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(141, 23);
            this.scLabelExt2.TabIndex = 1;
            this.scLabelExt2.Text = "Cantidad nueva : ";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCantidadAnterior
            // 
            this.lblCantidadAnterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCantidadAnterior.Location = new System.Drawing.Point(169, 32);
            this.lblCantidadAnterior.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCantidadAnterior.MostrarToolTip = true;
            this.lblCantidadAnterior.Name = "lblCantidadAnterior";
            this.lblCantidadAnterior.Size = new System.Drawing.Size(133, 23);
            this.lblCantidadAnterior.TabIndex = 3;
            this.lblCantidadAnterior.Text = "Cantidad anterior : ";
            this.lblCantidadAnterior.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(21, 32);
            this.scLabelExt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt1.MostrarToolTip = true;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(141, 23);
            this.scLabelExt1.TabIndex = 0;
            this.scLabelExt1.Text = "Cantidad anterior : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmModificarCantidadSurtida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 181);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmModificarCantidadSurtida";
            this.ShowIcon = false;
            this.Text = "Modificar cantidad surtida";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmModificarCantidadSurtida_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidadNueva)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private SC_ControlsCS.scLabelExt lblCantidadAnterior;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private SC_ControlsCS.scLabelExt scLabelExt3;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.NumericUpDown txtCantidadNueva;
    }
}