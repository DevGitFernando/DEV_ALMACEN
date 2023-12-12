namespace DllFarmaciaSoft.Lotes
{
    partial class FrmModificarCaducidades
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
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.dtpCaducidadNueva = new System.Windows.Forms.DateTimePicker();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.dtpCaducidadActual = new System.Windows.Forms.DateTimePicker();
            this.btnModificarCaducidad = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSubFarmacia = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblClaveLote = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblCodigoEAN = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblIdProducto = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scLabelExt2);
            this.groupBox1.Controls.Add(this.dtpCaducidadNueva);
            this.groupBox1.Controls.Add(this.scLabelExt1);
            this.groupBox1.Controls.Add(this.dtpCaducidadActual);
            this.groupBox1.Location = new System.Drawing.Point(8, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de caducidad";
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(158, 22);
            this.scLabelExt2.MostrarToolTip = true;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(47, 15);
            this.scLabelExt2.TabIndex = 3;
            this.scLabelExt2.Text = "Nueva : ";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpCaducidadNueva
            // 
            this.dtpCaducidadNueva.CustomFormat = "yyyy-MM";
            this.dtpCaducidadNueva.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCaducidadNueva.Location = new System.Drawing.Point(206, 19);
            this.dtpCaducidadNueva.Name = "dtpCaducidadNueva";
            this.dtpCaducidadNueva.ShowUpDown = true;
            this.dtpCaducidadNueva.Size = new System.Drawing.Size(70, 23);
            this.dtpCaducidadNueva.TabIndex = 1;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(21, 22);
            this.scLabelExt1.MostrarToolTip = true;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(47, 15);
            this.scLabelExt1.TabIndex = 1;
            this.scLabelExt1.Text = "Actual : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpCaducidadActual
            // 
            this.dtpCaducidadActual.CustomFormat = "yyyy-MM";
            this.dtpCaducidadActual.Enabled = false;
            this.dtpCaducidadActual.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCaducidadActual.Location = new System.Drawing.Point(69, 19);
            this.dtpCaducidadActual.Name = "dtpCaducidadActual";
            this.dtpCaducidadActual.ShowUpDown = true;
            this.dtpCaducidadActual.Size = new System.Drawing.Size(70, 23);
            this.dtpCaducidadActual.TabIndex = 0;
            // 
            // btnModificarCaducidad
            // 
            this.btnModificarCaducidad.Location = new System.Drawing.Point(146, 189);
            this.btnModificarCaducidad.Name = "btnModificarCaducidad";
            this.btnModificarCaducidad.Size = new System.Drawing.Size(78, 23);
            this.btnModificarCaducidad.TabIndex = 2;
            this.btnModificarCaducidad.Text = "[F5] Aceptar";
            this.btnModificarCaducidad.UseVisualStyleBackColor = true;
            this.btnModificarCaducidad.Click += new System.EventHandler(this.btnModificarCaducidad_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(227, 189);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(78, 23);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "[F12] Cerrar";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblSubFarmacia);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblClaveLote);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblCodigoEAN);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblIdProducto);
            this.groupBox2.Location = new System.Drawing.Point(8, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 124);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información del Producto";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Sub-Farmacia :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubFarmacia
            // 
            this.lblSubFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubFarmacia.Location = new System.Drawing.Point(96, 70);
            this.lblSubFarmacia.Name = "lblSubFarmacia";
            this.lblSubFarmacia.Size = new System.Drawing.Size(186, 20);
            this.lblSubFarmacia.TabIndex = 2;
            this.lblSubFarmacia.Text = "Producto :";
            this.lblSubFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "Clave de lote :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveLote
            // 
            this.lblClaveLote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveLote.Location = new System.Drawing.Point(96, 93);
            this.lblClaveLote.Name = "lblClaveLote";
            this.lblClaveLote.Size = new System.Drawing.Size(186, 20);
            this.lblClaveLote.TabIndex = 3;
            this.lblClaveLote.Text = "Producto :";
            this.lblClaveLote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(15, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 15);
            this.label11.TabIndex = 23;
            this.label11.Text = "Código EAN :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCodigoEAN
            // 
            this.lblCodigoEAN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCodigoEAN.Location = new System.Drawing.Point(96, 47);
            this.lblCodigoEAN.Name = "lblCodigoEAN";
            this.lblCodigoEAN.Size = new System.Drawing.Size(186, 20);
            this.lblCodigoEAN.TabIndex = 1;
            this.lblCodigoEAN.Text = "Producto :";
            this.lblCodigoEAN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Producto :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIdProducto
            // 
            this.lblIdProducto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdProducto.Location = new System.Drawing.Point(96, 23);
            this.lblIdProducto.Name = "lblIdProducto";
            this.lblIdProducto.Size = new System.Drawing.Size(186, 20);
            this.lblIdProducto.TabIndex = 0;
            this.lblIdProducto.Text = "Producto :";
            this.lblIdProducto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmModificarCaducidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 220);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnModificarCaducidad);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmModificarCaducidades";
            this.ShowIcon = false;
            this.Text = "Modificar caducidad";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmModificarCaducidades_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private System.Windows.Forms.DateTimePicker dtpCaducidadActual;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private System.Windows.Forms.DateTimePicker dtpCaducidadNueva;
        private System.Windows.Forms.Button btnModificarCaducidad;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblCodigoEAN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIdProducto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblClaveLote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSubFarmacia;
    }
}