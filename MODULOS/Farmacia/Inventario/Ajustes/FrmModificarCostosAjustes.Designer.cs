namespace Farmacia.Inventario
{
    partial class FrmModificarCostosAjustes
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
            this.lblCostoBase = new SC_ControlsCS.scLabelExt();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.txtCostoNuevo = new System.Windows.Forms.NumericUpDown();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.lblCostoAnteriorAnterior = new SC_ControlsCS.scLabelExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostoNuevo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCostoBase);
            this.groupBox1.Controls.Add(this.scLabelExt4);
            this.groupBox1.Controls.Add(this.txtCostoNuevo);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Controls.Add(this.scLabelExt2);
            this.groupBox1.Controls.Add(this.lblCostoAnteriorAnterior);
            this.groupBox1.Controls.Add(this.scLabelExt1);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 133);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // lblCostoBase
            // 
            this.lblCostoBase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCostoBase.Location = new System.Drawing.Point(127, 16);
            this.lblCostoBase.MostrarToolTip = false;
            this.lblCostoBase.Name = "lblCostoBase";
            this.lblCostoBase.Size = new System.Drawing.Size(100, 19);
            this.lblCostoBase.TabIndex = 9;
            this.lblCostoBase.Text = "Cantidad anterior : ";
            this.lblCostoBase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Location = new System.Drawing.Point(16, 16);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(106, 19);
            this.scLabelExt4.TabIndex = 8;
            this.scLabelExt4.Text = "Precio base : ";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCostoNuevo
            // 
            this.txtCostoNuevo.DecimalPlaces = 4;
            this.txtCostoNuevo.Location = new System.Drawing.Point(127, 65);
            this.txtCostoNuevo.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.txtCostoNuevo.Name = "txtCostoNuevo";
            this.txtCostoNuevo.Size = new System.Drawing.Size(100, 20);
            this.txtCostoNuevo.TabIndex = 7;
            this.txtCostoNuevo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(309, 101);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(91, 23);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "[F12] Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(216, 101);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(91, 23);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "[F5] Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(16, 65);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(106, 19);
            this.scLabelExt2.TabIndex = 1;
            this.scLabelExt2.Text = "Costo nuevo : ";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCostoAnteriorAnterior
            // 
            this.lblCostoAnteriorAnterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCostoAnteriorAnterior.Location = new System.Drawing.Point(127, 40);
            this.lblCostoAnteriorAnterior.MostrarToolTip = false;
            this.lblCostoAnteriorAnterior.Name = "lblCostoAnteriorAnterior";
            this.lblCostoAnteriorAnterior.Size = new System.Drawing.Size(100, 19);
            this.lblCostoAnteriorAnterior.TabIndex = 3;
            this.lblCostoAnteriorAnterior.Text = "Cantidad anterior : ";
            this.lblCostoAnteriorAnterior.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(16, 40);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(106, 19);
            this.scLabelExt1.TabIndex = 0;
            this.scLabelExt1.Text = "Costo anterior : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmModificarCostosAjustes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 147);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmModificarCostosAjustes";
            this.Text = "Modificar costo";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmModificarCostosAjustes_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCostoNuevo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private SC_ControlsCS.scLabelExt lblCostoAnteriorAnterior;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.NumericUpDown txtCostoNuevo;
        private SC_ControlsCS.scLabelExt lblCostoBase;
        private SC_ControlsCS.scLabelExt scLabelExt4;
    }
}