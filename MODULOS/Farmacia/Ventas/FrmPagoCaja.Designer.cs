namespace Farmacia.Ventas
{
    partial class FrmPagoCaja
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblSubTotalIva0 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCheques = new SC_ControlsCS.scCurrencyTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalPago = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDolares = new SC_ControlsCS.scCurrencyTextBox();
            this.txtEfectivo = new SC_ControlsCS.scCurrencyTextBox();
            this.lblCambio = new System.Windows.Forms.Label();
            this.lblTipoDeCambio = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotal);
            this.groupBox1.Controls.Add(this.lblIva);
            this.groupBox1.Controls.Add(this.lblSubTotal);
            this.groupBox1.Controls.Add(this.lblSubTotalIva0);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 117);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de Venta";
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTotal.Location = new System.Drawing.Point(151, 79);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(127, 25);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total :";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva
            // 
            this.lblIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(151, 51);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(127, 23);
            this.lblIva.TabIndex = 2;
            this.lblIva.Text = "Iva :";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(151, 23);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(127, 23);
            this.lblSubTotal.TabIndex = 1;
            this.lblSubTotal.Text = "Sub-Total :";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotalIva0
            // 
            this.lblSubTotalIva0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotalIva0.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotalIva0.Location = new System.Drawing.Point(151, 117);
            this.lblSubTotalIva0.Name = "lblSubTotalIva0";
            this.lblSubTotalIva0.Size = new System.Drawing.Size(127, 23);
            this.lblSubTotalIva0.TabIndex = 0;
            this.lblSubTotalIva0.Text = "Sub-Total Iva 0 :";
            this.lblSubTotalIva0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSubTotalIva0.Visible = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Total :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Iva :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sub-Total :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sub-Total Iva 0 :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCheques);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lblTotalPago);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtDolares);
            this.groupBox2.Controls.Add(this.txtEfectivo);
            this.groupBox2.Controls.Add(this.lblCambio);
            this.groupBox2.Controls.Add(this.lblTipoDeCambio);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(11, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 221);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información de Pago";
            // 
            // txtCheques
            // 
            this.txtCheques.AllowNegative = false;
            this.txtCheques.DigitsInGroup = 3;
            this.txtCheques.Flags = 73216;
            this.txtCheques.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheques.Location = new System.Drawing.Point(151, 89);
            this.txtCheques.MaxWholeDigits = 9;
            this.txtCheques.Name = "txtCheques";
            this.txtCheques.RangeMax = 1.7976931348623157E+308;
            this.txtCheques.RangeMin = -1.7976931348623157E+308;
            this.txtCheques.Size = new System.Drawing.Size(127, 26);
            this.txtCheques.TabIndex = 2;
            this.txtCheques.Text = "1.00";
            this.txtCheques.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCheques.Validated += new System.EventHandler(this.txtCheques_Validated);
            this.txtCheques.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtCheques_MouseClick);
            this.txtCheques.Enter += new System.EventHandler(this.txtCheques_Enter);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 23);
            this.label5.TabIndex = 12;
            this.label5.Text = "Cheques :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalPago
            // 
            this.lblTotalPago.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPago.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTotalPago.Location = new System.Drawing.Point(151, 151);
            this.lblTotalPago.Name = "lblTotalPago";
            this.lblTotalPago.Size = new System.Drawing.Size(127, 25);
            this.lblTotalPago.TabIndex = 4;
            this.lblTotalPago.Text = "Total :";
            this.lblTotalPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 23);
            this.label6.TabIndex = 11;
            this.label6.Text = "Total pago :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDolares
            // 
            this.txtDolares.AllowNegative = false;
            this.txtDolares.DigitsInGroup = 3;
            this.txtDolares.Flags = 73216;
            this.txtDolares.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDolares.Location = new System.Drawing.Point(151, 121);
            this.txtDolares.MaxWholeDigits = 9;
            this.txtDolares.Name = "txtDolares";
            this.txtDolares.RangeMax = 1.7976931348623157E+308;
            this.txtDolares.RangeMin = -1.7976931348623157E+308;
            this.txtDolares.Size = new System.Drawing.Size(127, 26);
            this.txtDolares.TabIndex = 3;
            this.txtDolares.Text = "1.00";
            this.txtDolares.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDolares.Validated += new System.EventHandler(this.txtDolares_Validated);
            this.txtDolares.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtDolares_MouseClick);
            this.txtDolares.Enter += new System.EventHandler(this.txtDolares_Enter);
            // 
            // txtEfectivo
            // 
            this.txtEfectivo.AllowNegative = false;
            this.txtEfectivo.DigitsInGroup = 3;
            this.txtEfectivo.Flags = 73216;
            this.txtEfectivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEfectivo.Location = new System.Drawing.Point(151, 57);
            this.txtEfectivo.MaxWholeDigits = 9;
            this.txtEfectivo.Name = "txtEfectivo";
            this.txtEfectivo.RangeMax = 1.7976931348623157E+308;
            this.txtEfectivo.RangeMin = -1.7976931348623157E+308;
            this.txtEfectivo.Size = new System.Drawing.Size(127, 26);
            this.txtEfectivo.TabIndex = 1;
            this.txtEfectivo.Text = "1.00";
            this.txtEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEfectivo.Validated += new System.EventHandler(this.txtEfectivo_Validated);
            this.txtEfectivo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtEfectivo_MouseClick);
            this.txtEfectivo.Enter += new System.EventHandler(this.txtEfectivo_Enter);
            // 
            // lblCambio
            // 
            this.lblCambio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCambio.ForeColor = System.Drawing.Color.DarkRed;
            this.lblCambio.Location = new System.Drawing.Point(151, 185);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(127, 25);
            this.lblCambio.TabIndex = 5;
            this.lblCambio.Text = "Total :";
            this.lblCambio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTipoDeCambio
            // 
            this.lblTipoDeCambio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTipoDeCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoDeCambio.Location = new System.Drawing.Point(151, 29);
            this.lblTipoDeCambio.Name = "lblTipoDeCambio";
            this.lblTipoDeCambio.Size = new System.Drawing.Size(127, 23);
            this.lblTipoDeCambio.TabIndex = 0;
            this.lblTipoDeCambio.Text = "Sub-Total Iva 0 :";
            this.lblTipoDeCambio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(18, 185);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 23);
            this.label9.TabIndex = 3;
            this.label9.Text = "Cambio :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(18, 121);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 23);
            this.label10.TabIndex = 2;
            this.label10.Text = "Dolares :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(18, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(127, 23);
            this.label11.TabIndex = 1;
            this.label11.Text = "Efectivo :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(18, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 23);
            this.label12.TabIndex = 0;
            this.label12.Text = "Tipo de Cambio :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(0, 356);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(318, 24);
            this.label7.TabIndex = 11;
            this.label7.Text = " <F5> Pagar                                   <F12> Salir";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmPagoCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 380);
            this.ControlBox = false;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmPagoCaja";
            this.Text = "Capturar pago de Venta";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPagoCaja_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPagoCaja_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblSubTotalIva0;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblCambio;
        private System.Windows.Forms.Label lblTipoDeCambio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private SC_ControlsCS.scCurrencyTextBox txtEfectivo;
        private SC_ControlsCS.scCurrencyTextBox txtDolares;
        private System.Windows.Forms.Label lblTotalPago;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scCurrencyTextBox txtCheques;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}