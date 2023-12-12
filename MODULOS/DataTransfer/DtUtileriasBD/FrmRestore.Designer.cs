namespace DtUtileriasBD
{
    partial class FrmRestore
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRutaRespaldos = new SC_ControlsCS.scLabelExt();
            this.btnRuta = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRegistro = new SC_ControlsCS.scLabelExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtData = new SC_ControlsCS.scLabelExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtRutaRestore = new SC_ControlsCS.scLabelExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNombreBD = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRutaRestore = new System.Windows.Forms.Button();
            this.btnRestaurarBaseDeDatos = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRutaRespaldos);
            this.groupBox2.Controls.Add(this.btnRuta);
            this.groupBox2.Location = new System.Drawing.Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(570, 52);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ruta donde se encuentra el Archivo de Respaldo";
            // 
            // txtRutaRespaldos
            // 
            this.txtRutaRespaldos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtRutaRespaldos.Location = new System.Drawing.Point(10, 20);
            this.txtRutaRespaldos.MostrarToolTip = false;
            this.txtRutaRespaldos.Name = "txtRutaRespaldos";
            this.txtRutaRespaldos.Size = new System.Drawing.Size(522, 20);
            this.txtRutaRespaldos.TabIndex = 4;
            this.txtRutaRespaldos.Text = "scLabelExt1";
            this.txtRutaRespaldos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRuta
            // 
            this.btnRuta.Location = new System.Drawing.Point(536, 18);
            this.btnRuta.Name = "btnRuta";
            this.btnRuta.Size = new System.Drawing.Size(26, 19);
            this.btnRuta.TabIndex = 3;
            this.btnRuta.Text = "...";
            this.btnRuta.UseVisualStyleBackColor = true;
            this.btnRuta.Click += new System.EventHandler(this.btnRuta_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRegistro);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtData);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 50);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Respaldo";
            // 
            // txtRegistro
            // 
            this.txtRegistro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtRegistro.Location = new System.Drawing.Point(347, 19);
            this.txtRegistro.MostrarToolTip = false;
            this.txtRegistro.Name = "txtRegistro";
            this.txtRegistro.Size = new System.Drawing.Size(214, 20);
            this.txtRegistro.TabIndex = 7;
            this.txtRegistro.Text = "scLabelExt2";
            this.txtRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(286, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Registro :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtData
            // 
            this.txtData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtData.Location = new System.Drawing.Point(71, 19);
            this.txtData.MostrarToolTip = false;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(214, 20);
            this.txtData.TabIndex = 6;
            this.txtData.Text = "scLabelExt1";
            this.txtData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(28, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Data :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtRutaRestore);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtNombreBD);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnRutaRestore);
            this.groupBox3.Location = new System.Drawing.Point(8, 122);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(570, 79);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Base de Datos";
            // 
            // txtRutaRestore
            // 
            this.txtRutaRestore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtRutaRestore.Location = new System.Drawing.Point(71, 16);
            this.txtRutaRestore.MostrarToolTip = false;
            this.txtRutaRestore.Name = "txtRutaRestore";
            this.txtRutaRestore.Size = new System.Drawing.Size(461, 20);
            this.txtRutaRestore.TabIndex = 5;
            this.txtRutaRestore.Text = "scLabelExt1";
            this.txtRutaRestore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nombre :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNombreBD
            // 
            this.txtNombreBD.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombreBD.Decimales = 2;
            this.txtNombreBD.Enabled = false;
            this.txtNombreBD.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombreBD.ForeColor = System.Drawing.Color.Black;
            this.txtNombreBD.Location = new System.Drawing.Point(71, 45);
            this.txtNombreBD.MostrarToolTip = false;
            this.txtNombreBD.Name = "txtNombreBD";
            this.txtNombreBD.PermitirApostrofo = false;
            this.txtNombreBD.PermitirNegativos = false;
            this.txtNombreBD.Size = new System.Drawing.Size(270, 20);
            this.txtNombreBD.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(28, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ruta :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRutaRestore
            // 
            this.btnRutaRestore.Location = new System.Drawing.Point(536, 18);
            this.btnRutaRestore.Name = "btnRutaRestore";
            this.btnRutaRestore.Size = new System.Drawing.Size(26, 19);
            this.btnRutaRestore.TabIndex = 3;
            this.btnRutaRestore.Text = "...";
            this.btnRutaRestore.UseVisualStyleBackColor = true;
            this.btnRutaRestore.Click += new System.EventHandler(this.btnRutaRestore_Click);
            // 
            // btnRestaurarBaseDeDatos
            // 
            this.btnRestaurarBaseDeDatos.Location = new System.Drawing.Point(384, 207);
            this.btnRestaurarBaseDeDatos.Name = "btnRestaurarBaseDeDatos";
            this.btnRestaurarBaseDeDatos.Size = new System.Drawing.Size(195, 23);
            this.btnRestaurarBaseDeDatos.TabIndex = 5;
            this.btnRestaurarBaseDeDatos.Text = "Restaurar Base de Datos";
            this.btnRestaurarBaseDeDatos.UseVisualStyleBackColor = true;
            this.btnRestaurarBaseDeDatos.Click += new System.EventHandler(this.btnRestaurarBaseDeDatos_Click);
            // 
            // FrmRestore
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 242);
            this.Controls.Add(this.btnRestaurarBaseDeDatos);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmRestore";
            this.Text = "Restaurar Base de Datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRestore_FormClosing);
            this.Load += new System.EventHandler(this.FrmRestore_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmRestore_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmRestore_DragEnter);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRuta;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRutaRestore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtNombreBD;
        private System.Windows.Forms.Button btnRestaurarBaseDeDatos;
        private SC_ControlsCS.scLabelExt txtRutaRespaldos;
        private SC_ControlsCS.scLabelExt txtData;
        private SC_ControlsCS.scLabelExt txtRegistro;
        private SC_ControlsCS.scLabelExt txtRutaRestore;
    }
}