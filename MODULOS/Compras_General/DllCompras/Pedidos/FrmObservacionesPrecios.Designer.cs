namespace DllCompras.Pedidos
{
    partial class FrmObservacionesPrecios
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.FrameObservaciones = new System.Windows.Forms.GroupBox();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.lblDescripcionEAN = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCodigoEAN = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProv = new System.Windows.Forms.Label();
            this.lblNomProv = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPrecioMin = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FrameObservaciones.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(471, 251);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(104, 30);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FrameObservaciones
            // 
            this.FrameObservaciones.Controls.Add(this.txtObservaciones);
            this.FrameObservaciones.Location = new System.Drawing.Point(12, 146);
            this.FrameObservaciones.Name = "FrameObservaciones";
            this.FrameObservaciones.Size = new System.Drawing.Size(693, 99);
            this.FrameObservaciones.TabIndex = 3;
            this.FrameObservaciones.TabStop = false;
            this.FrameObservaciones.Text = "Observaciones";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(12, 18);
            this.txtObservaciones.MaxLength = 500;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(671, 70);
            this.txtObservaciones.TabIndex = 0;
            // 
            // lblDescripcionEAN
            // 
            this.lblDescripcionEAN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionEAN.Location = new System.Drawing.Point(99, 71);
            this.lblDescripcionEAN.Name = "lblDescripcionEAN";
            this.lblDescripcionEAN.Size = new System.Drawing.Size(581, 56);
            this.lblDescripcionEAN.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(17, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "CódigoEAN :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCodigoEAN
            // 
            this.lblCodigoEAN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCodigoEAN.Location = new System.Drawing.Point(99, 45);
            this.lblCodigoEAN.Name = "lblCodigoEAN";
            this.lblCodigoEAN.Size = new System.Drawing.Size(118, 20);
            this.lblCodigoEAN.TabIndex = 18;
            this.lblCodigoEAN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Proveedor :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProv
            // 
            this.lblProv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProv.Location = new System.Drawing.Point(99, 17);
            this.lblProv.Name = "lblProv";
            this.lblProv.Size = new System.Drawing.Size(118, 20);
            this.lblProv.TabIndex = 21;
            this.lblProv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNomProv
            // 
            this.lblNomProv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomProv.Location = new System.Drawing.Point(223, 17);
            this.lblNomProv.Name = "lblNomProv";
            this.lblNomProv.Size = new System.Drawing.Size(457, 20);
            this.lblNomProv.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(221, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "Precio :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrecio
            // 
            this.lblPrecio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrecio.Location = new System.Drawing.Point(285, 45);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(118, 20);
            this.lblPrecio.TabIndex = 24;
            this.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(459, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "Precio Mínimo :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrecioMin
            // 
            this.lblPrecioMin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrecioMin.Location = new System.Drawing.Point(562, 45);
            this.lblPrecioMin.Name = "lblPrecioMin";
            this.lblPrecioMin.Size = new System.Drawing.Size(118, 20);
            this.lblPrecioMin.TabIndex = 26;
            this.lblPrecioMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(591, 251);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(104, 30);
            this.btnCancelar.TabIndex = 28;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDescripcionEAN);
            this.groupBox1.Controls.Add(this.lblCodigoEAN);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblPrecioMin);
            this.groupBox1.Controls.Add(this.lblProv);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblPrecio);
            this.groupBox1.Controls.Add(this.lblNomProv);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 140);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // FrmObservacionesPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 293);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.FrameObservaciones);
            this.Name = "FrmObservacionesPrecios";
            this.Text = "Observaciones de Precios Elevados";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmObservacionesPrecios_Load);
            this.FrameObservaciones.ResumeLayout(false);
            this.FrameObservaciones.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox FrameObservaciones;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label lblDescripcionEAN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCodigoEAN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProv;
        private System.Windows.Forms.Label lblNomProv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPrecioMin;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}