namespace DtUtileriasBD
{
    partial class FrmBackUp
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
            this.cboBasesDeDatos = new SC_ControlsCS.scComboBoxExt();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRuta = new System.Windows.Forms.Button();
            this.txtRutaRespaldos = new SC_ControlsCS.scTextBoxExt();
            this.btnGenerarRespaldo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cboBasesDeDatos);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista de Bases de Datos disponibles";
            // 
            // cboBasesDeDatos
            // 
            this.cboBasesDeDatos.BackColorEnabled = System.Drawing.Color.White;
            this.cboBasesDeDatos.Data = "";
            this.cboBasesDeDatos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBasesDeDatos.Filtro = " 1 = 1";
            this.cboBasesDeDatos.FormattingEnabled = true;
            this.cboBasesDeDatos.ListaItemsBusqueda = 5;
            this.cboBasesDeDatos.Location = new System.Drawing.Point(10, 19);
            this.cboBasesDeDatos.MostrarToolTip = false;
            this.cboBasesDeDatos.Name = "cboBasesDeDatos";
            this.cboBasesDeDatos.Size = new System.Drawing.Size(522, 21);
            this.cboBasesDeDatos.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRuta);
            this.groupBox2.Controls.Add(this.txtRutaRespaldos);
            this.groupBox2.Location = new System.Drawing.Point(8, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(570, 47);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ruta para guardar el Respado de Bases de Datos disponibles";
            // 
            // btnRuta
            // 
            this.btnRuta.Location = new System.Drawing.Point(536, 19);
            this.btnRuta.Name = "btnRuta";
            this.btnRuta.Size = new System.Drawing.Size(26, 19);
            this.btnRuta.TabIndex = 1;
            this.btnRuta.Text = "...";
            this.btnRuta.UseVisualStyleBackColor = true;
            this.btnRuta.Click += new System.EventHandler(this.btnRuta_Click);
            // 
            // txtRutaRespaldos
            // 
            this.txtRutaRespaldos.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRutaRespaldos.Decimales = 2;
            this.txtRutaRespaldos.Enabled = false;
            this.txtRutaRespaldos.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRutaRespaldos.ForeColor = System.Drawing.Color.Black;
            this.txtRutaRespaldos.Location = new System.Drawing.Point(10, 19);
            this.txtRutaRespaldos.Name = "txtRutaRespaldos";
            this.txtRutaRespaldos.PermitirApostrofo = false;
            this.txtRutaRespaldos.PermitirNegativos = false;
            this.txtRutaRespaldos.Size = new System.Drawing.Size(522, 20);
            this.txtRutaRespaldos.TabIndex = 0;
            // 
            // btnGenerarRespaldo
            // 
            this.btnGenerarRespaldo.Location = new System.Drawing.Point(383, 118);
            this.btnGenerarRespaldo.Name = "btnGenerarRespaldo";
            this.btnGenerarRespaldo.Size = new System.Drawing.Size(195, 23);
            this.btnGenerarRespaldo.TabIndex = 2;
            this.btnGenerarRespaldo.Text = "Generar respaldo";
            this.btnGenerarRespaldo.UseVisualStyleBackColor = true;
            this.btnGenerarRespaldo.Click += new System.EventHandler(this.btnGenerarRespaldo_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(536, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 19);
            this.button1.TabIndex = 1;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmBackUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 149);
            this.Controls.Add(this.btnGenerarRespaldo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmBackUp";
            this.Text = "Generar respaldos de Base de Datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBackUp_FormClosing);
            this.Load += new System.EventHandler(this.FrmBackUp_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scComboBoxExt cboBasesDeDatos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGenerarRespaldo;
        private SC_ControlsCS.scTextBoxExt txtRutaRespaldos;
        private System.Windows.Forms.Button btnRuta;
        private System.Windows.Forms.Button button1;
    }
}