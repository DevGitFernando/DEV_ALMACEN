namespace Farmacia.VentasDispensacion
{
    partial class FrmPDD_06_LeerLotes
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
            this.txtCodigo = new SC_ControlsCS.scTextBoxExt();
            this.txtCodigoLote = new SC_ControlsCS.scTextBoxExt();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.FrameLotes = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstLotes = new System.Windows.Forms.ListView();
            this.SubFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Existencia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cantidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblLecturaLote = new System.Windows.Forms.Label();
            this.lblVerLotes = new System.Windows.Forms.Label();
            this.lblVacio = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.FrameLotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.txtCodigoLote);
            this.groupBox1.Location = new System.Drawing.Point(8, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtCodigo
            // 
            this.txtCodigo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigo.Decimales = 2;
            this.txtCodigo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigo.ForeColor = System.Drawing.Color.Black;
            this.txtCodigo.Location = new System.Drawing.Point(7, 90);
            this.txtCodigo.MaxLength = 15;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PermitirApostrofo = false;
            this.txtCodigo.PermitirNegativos = false;
            this.txtCodigo.Size = new System.Drawing.Size(119, 20);
            this.txtCodigo.TabIndex = 33;
            this.txtCodigo.Text = "01234567890123";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCodigoLote
            // 
            this.txtCodigoLote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigoLote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoLote.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoLote.Decimales = 2;
            this.txtCodigoLote.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCodigoLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoLote.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoLote.Location = new System.Drawing.Point(10, 15);
            this.txtCodigoLote.MaxLength = 23;
            this.txtCodigoLote.Name = "txtCodigoLote";
            this.txtCodigoLote.PermitirApostrofo = false;
            this.txtCodigoLote.PermitirNegativos = false;
            this.txtCodigoLote.Size = new System.Drawing.Size(496, 47);
            this.txtCodigoLote.TabIndex = 0;
            this.txtCodigoLote.Text = "012345678901234567890123";
            this.txtCodigoLote.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoLote.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoLote_Validating);
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(56, 294);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(110, 24);
            this.lblMensajes.TabIndex = 11;
            this.lblMensajes.Text = "<F12> Cerrar";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMensajes.Click += new System.EventHandler(this.lblMensajes_Click);
            // 
            // FrameLotes
            // 
            this.FrameLotes.Controls.Add(this.label2);
            this.FrameLotes.Controls.Add(this.lstLotes);
            this.FrameLotes.Controls.Add(this.lblLecturaLote);
            this.FrameLotes.Location = new System.Drawing.Point(8, 75);
            this.FrameLotes.Name = "FrameLotes";
            this.FrameLotes.Size = new System.Drawing.Size(516, 216);
            this.FrameLotes.TabIndex = 12;
            this.FrameLotes.TabStop = false;
            this.FrameLotes.Text = "Lotes Capturados";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 28);
            this.label2.TabIndex = 52;
            this.label2.Text = "Lectura Lote :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstLotes
            // 
            this.lstLotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLotes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SubFarmacia,
            this.Lote,
            this.Existencia,
            this.Cantidad});
            this.lstLotes.Location = new System.Drawing.Point(10, 19);
            this.lstLotes.Name = "lstLotes";
            this.lstLotes.Size = new System.Drawing.Size(496, 154);
            this.lstLotes.TabIndex = 0;
            this.lstLotes.UseCompatibleStateImageBehavior = false;
            this.lstLotes.View = System.Windows.Forms.View.Details;
            // 
            // SubFarmacia
            // 
            this.SubFarmacia.Text = "Sub-Farmacia";
            this.SubFarmacia.Width = 170;
            // 
            // Lote
            // 
            this.Lote.Text = "Clave Lote";
            this.Lote.Width = 160;
            // 
            // Existencia
            // 
            this.Existencia.Text = "Existencia";
            this.Existencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Existencia.Width = 80;
            // 
            // Cantidad
            // 
            this.Cantidad.Text = "Cantidad";
            this.Cantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Cantidad.Width = 80;
            // 
            // lblLecturaLote
            // 
            this.lblLecturaLote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLecturaLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLecturaLote.Location = new System.Drawing.Point(113, 179);
            this.lblLecturaLote.Name = "lblLecturaLote";
            this.lblLecturaLote.Size = new System.Drawing.Size(393, 31);
            this.lblLecturaLote.TabIndex = 34;
            this.lblLecturaLote.Text = "Lectura Lote :";
            this.lblLecturaLote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVerLotes
            // 
            this.lblVerLotes.BackColor = System.Drawing.Color.Black;
            this.lblVerLotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerLotes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblVerLotes.Location = new System.Drawing.Point(290, 294);
            this.lblVerLotes.Name = "lblVerLotes";
            this.lblVerLotes.Size = new System.Drawing.Size(127, 24);
            this.lblVerLotes.TabIndex = 13;
            this.lblVerLotes.Text = "<F7> Ver Lotes ";
            this.lblVerLotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblVerLotes.Click += new System.EventHandler(this.lblVerLotes_Click);
            // 
            // lblVacio
            // 
            this.lblVacio.BackColor = System.Drawing.Color.Black;
            this.lblVacio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVacio.ForeColor = System.Drawing.SystemColors.Control;
            this.lblVacio.Location = new System.Drawing.Point(183, 294);
            this.lblVacio.Name = "lblVacio";
            this.lblVacio.Size = new System.Drawing.Size(87, 24);
            this.lblVacio.TabIndex = 14;
            this.lblVacio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmPDD_06_LeerLotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 320);
            this.Controls.Add(this.lblVacio);
            this.Controls.Add(this.lblVerLotes);
            this.Controls.Add(this.FrameLotes);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmPDD_06_LeerLotes";
            this.Text = "Lectura de código";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPDD_06_LeerLotes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPDD_06_LeerLotes_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameLotes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMensajes;
        private SC_ControlsCS.scTextBoxExt txtCodigoLote;
        private SC_ControlsCS.scTextBoxExt txtCodigo;
        private System.Windows.Forms.GroupBox FrameLotes;
        private System.Windows.Forms.ListView lstLotes;
        private System.Windows.Forms.ColumnHeader SubFarmacia;
        private System.Windows.Forms.ColumnHeader Lote;
        private System.Windows.Forms.ColumnHeader Cantidad;
        private System.Windows.Forms.Label lblVerLotes;
        private System.Windows.Forms.Label lblVacio;
        private System.Windows.Forms.Label lblLecturaLote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader Existencia;
    }
}