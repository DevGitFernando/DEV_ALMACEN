namespace TEST_ISESEQ
{
    partial class Form1
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
            this.txtXML = new System.Windows.Forms.TextBox();
            this.btnEnviarReceta = new System.Windows.Forms.Button();
            this.txtRespuesta = new System.Windows.Forms.TextBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtURL_Envio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtXML
            // 
            this.txtXML.Location = new System.Drawing.Point(11, 185);
            this.txtXML.Multiline = true;
            this.txtXML.Name = "txtXML";
            this.txtXML.Size = new System.Drawing.Size(860, 236);
            this.txtXML.TabIndex = 0;
            this.txtXML.TextChanged += new System.EventHandler(this.txtXML_TextChanged);
            // 
            // btnEnviarReceta
            // 
            this.btnEnviarReceta.Location = new System.Drawing.Point(12, 12);
            this.btnEnviarReceta.Name = "btnEnviarReceta";
            this.btnEnviarReceta.Size = new System.Drawing.Size(215, 23);
            this.btnEnviarReceta.TabIndex = 1;
            this.btnEnviarReceta.Text = "Enviar receta";
            this.btnEnviarReceta.UseVisualStyleBackColor = true;
            this.btnEnviarReceta.Click += new System.EventHandler(this.btnEnviarReceta_Click);
            // 
            // txtRespuesta
            // 
            this.txtRespuesta.Location = new System.Drawing.Point(11, 451);
            this.txtRespuesta.Multiline = true;
            this.txtRespuesta.Name = "txtRespuesta";
            this.txtRespuesta.Size = new System.Drawing.Size(860, 44);
            this.txtRespuesta.TabIndex = 2;
            // 
            // lblURL
            // 
            this.lblURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblURL.Location = new System.Drawing.Point(12, 38);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(860, 42);
            this.lblURL.TabIndex = 3;
            this.lblURL.Text = "label1";
            this.lblURL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblURL.Click += new System.EventHandler(this.lblURL_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(227, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Enviar acuse receta";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(657, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(215, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Enviar acuse cancelación de receta";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(442, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(215, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Enviar cancelación de receta";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtURL_Envio
            // 
            this.txtURL_Envio.Location = new System.Drawing.Point(11, 114);
            this.txtURL_Envio.Multiline = true;
            this.txtURL_Envio.Name = "txtURL_Envio";
            this.txtURL_Envio.Size = new System.Drawing.Size(860, 44);
            this.txtURL_Envio.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 97);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Url destino";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 169);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "XML";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 434);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Respuesta";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 506);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtURL_Envio);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.txtRespuesta);
            this.Controls.Add(this.btnEnviarReceta);
            this.Controls.Add(this.txtXML);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtXML;
        private System.Windows.Forms.Button btnEnviarReceta;
        private System.Windows.Forms.TextBox txtRespuesta;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtURL_Envio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

