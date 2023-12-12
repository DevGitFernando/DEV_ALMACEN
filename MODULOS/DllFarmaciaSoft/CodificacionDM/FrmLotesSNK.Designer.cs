namespace DllFarmaciaSoft
{
    partial class FrmLotesSNK
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
            this.txtCodigo = new SC_ControlsCS.scTextBoxExt();
            this.txtCodigoLote = new SC_ControlsCS.scTextBoxExt();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.FrameLotes = new System.Windows.Forms.GroupBox();
            this.gpoUbicaciones = new System.Windows.Forms.GroupBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.lblEntrepaño = new System.Windows.Forms.Label();
            this.txtEntrepaño = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.lblEstante = new System.Windows.Forms.Label();
            this.txtEstante = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPasillo = new System.Windows.Forms.Label();
            this.txtPasillo = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstLotes = new System.Windows.Forms.ListView();
            this.SubFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Existencia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cantidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblLecturaLote = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.FrameLotes.SuspendLayout();
            this.gpoUbicaciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.txtCodigoLote);
            this.groupBox1.Location = new System.Drawing.Point(8, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(801, 70);
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
            this.txtCodigoLote.MaxLength = 100;
            this.txtCodigoLote.Name = "txtCodigoLote";
            this.txtCodigoLote.PasswordChar = '*';
            this.txtCodigoLote.PermitirApostrofo = false;
            this.txtCodigoLote.PermitirNegativos = false;
            this.txtCodigoLote.Size = new System.Drawing.Size(781, 47);
            this.txtCodigoLote.TabIndex = 0;
            this.txtCodigoLote.Text = "012345678901234567890123";
            this.txtCodigoLote.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoLote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoLote_KeyDown);
            this.txtCodigoLote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoLote_KeyPress);
            this.txtCodigoLote.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoLote_Validating);
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 405);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(815, 24);
            this.lblMensajes.TabIndex = 11;
            this.lblMensajes.Text = "<F12> Cerrar   ";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMensajes.Click += new System.EventHandler(this.lblMensajes_Click);
            // 
            // FrameLotes
            // 
            this.FrameLotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameLotes.Controls.Add(this.gpoUbicaciones);
            this.FrameLotes.Controls.Add(this.label2);
            this.FrameLotes.Controls.Add(this.lstLotes);
            this.FrameLotes.Controls.Add(this.lblLecturaLote);
            this.FrameLotes.Location = new System.Drawing.Point(8, 75);
            this.FrameLotes.Name = "FrameLotes";
            this.FrameLotes.Size = new System.Drawing.Size(801, 325);
            this.FrameLotes.TabIndex = 1;
            this.FrameLotes.TabStop = false;
            this.FrameLotes.Text = "Lotes Capturados";
            // 
            // gpoUbicaciones
            // 
            this.gpoUbicaciones.Controls.Add(this.btnAgregar);
            this.gpoUbicaciones.Controls.Add(this.lblEntrepaño);
            this.gpoUbicaciones.Controls.Add(this.txtEntrepaño);
            this.gpoUbicaciones.Controls.Add(this.label10);
            this.gpoUbicaciones.Controls.Add(this.lblEstante);
            this.gpoUbicaciones.Controls.Add(this.txtEstante);
            this.gpoUbicaciones.Controls.Add(this.label3);
            this.gpoUbicaciones.Controls.Add(this.lblPasillo);
            this.gpoUbicaciones.Controls.Add(this.txtPasillo);
            this.gpoUbicaciones.Controls.Add(this.label6);
            this.gpoUbicaciones.Location = new System.Drawing.Point(215, 92);
            this.gpoUbicaciones.Name = "gpoUbicaciones";
            this.gpoUbicaciones.Size = new System.Drawing.Size(448, 151);
            this.gpoUbicaciones.TabIndex = 0;
            this.gpoUbicaciones.TabStop = false;
            this.gpoUbicaciones.Text = "Datos de ubicacion";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(287, 135);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(79, 23);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Continuar ";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lblEntrepaño
            // 
            this.lblEntrepaño.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEntrepaño.Location = new System.Drawing.Point(287, 112);
            this.lblEntrepaño.Name = "lblEntrepaño";
            this.lblEntrepaño.Size = new System.Drawing.Size(260, 20);
            this.lblEntrepaño.TabIndex = 56;
            this.lblEntrepaño.Text = "label1";
            // 
            // txtEntrepaño
            // 
            this.txtEntrepaño.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEntrepaño.Decimales = 2;
            this.txtEntrepaño.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEntrepaño.ForeColor = System.Drawing.Color.Black;
            this.txtEntrepaño.Location = new System.Drawing.Point(222, 112);
            this.txtEntrepaño.MaxLength = 4;
            this.txtEntrepaño.Name = "txtEntrepaño";
            this.txtEntrepaño.PermitirApostrofo = false;
            this.txtEntrepaño.PermitirNegativos = false;
            this.txtEntrepaño.Size = new System.Drawing.Size(59, 20);
            this.txtEntrepaño.TabIndex = 2;
            this.txtEntrepaño.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEntrepaño.TextChanged += new System.EventHandler(this.txtEntrepaño_TextChanged);
            this.txtEntrepaño.Validating += new System.ComponentModel.CancelEventHandler(this.txtEntrepaño_Validating);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(127, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 16);
            this.label10.TabIndex = 55;
            this.label10.Text = "Entrepaño :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstante
            // 
            this.lblEstante.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstante.Location = new System.Drawing.Point(287, 87);
            this.lblEstante.Name = "lblEstante";
            this.lblEstante.Size = new System.Drawing.Size(260, 20);
            this.lblEstante.TabIndex = 53;
            this.lblEstante.Text = "label1";
            // 
            // txtEstante
            // 
            this.txtEstante.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEstante.Decimales = 2;
            this.txtEstante.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEstante.ForeColor = System.Drawing.Color.Black;
            this.txtEstante.Location = new System.Drawing.Point(222, 87);
            this.txtEstante.MaxLength = 4;
            this.txtEstante.Name = "txtEstante";
            this.txtEstante.PermitirApostrofo = false;
            this.txtEstante.PermitirNegativos = false;
            this.txtEstante.Size = new System.Drawing.Size(59, 20);
            this.txtEstante.TabIndex = 1;
            this.txtEstante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEstante.TextChanged += new System.EventHandler(this.txtEstante_TextChanged);
            this.txtEstante.Validating += new System.ComponentModel.CancelEventHandler(this.txtEstante_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(170, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 16);
            this.label3.TabIndex = 52;
            this.label3.Text = "Nivel :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPasillo
            // 
            this.lblPasillo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPasillo.Location = new System.Drawing.Point(287, 62);
            this.lblPasillo.Name = "lblPasillo";
            this.lblPasillo.Size = new System.Drawing.Size(260, 20);
            this.lblPasillo.TabIndex = 50;
            this.lblPasillo.Text = "label1";
            // 
            // txtPasillo
            // 
            this.txtPasillo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPasillo.Decimales = 2;
            this.txtPasillo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPasillo.ForeColor = System.Drawing.Color.Black;
            this.txtPasillo.Location = new System.Drawing.Point(222, 62);
            this.txtPasillo.MaxLength = 4;
            this.txtPasillo.Name = "txtPasillo";
            this.txtPasillo.PermitirApostrofo = false;
            this.txtPasillo.PermitirNegativos = false;
            this.txtPasillo.Size = new System.Drawing.Size(59, 20);
            this.txtPasillo.TabIndex = 0;
            this.txtPasillo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPasillo.TextChanged += new System.EventHandler(this.txtPasillo_TextChanged);
            this.txtPasillo.Validating += new System.ComponentModel.CancelEventHandler(this.txtPasillo_Validating);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(170, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 16);
            this.label6.TabIndex = 49;
            this.label6.Text = "Rack :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 28);
            this.label2.TabIndex = 52;
            this.label2.Text = "Resultado :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.lstLotes.Size = new System.Drawing.Size(781, 263);
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
            this.lblLecturaLote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLecturaLote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLecturaLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLecturaLote.Location = new System.Drawing.Point(90, 288);
            this.lblLecturaLote.Name = "lblLecturaLote";
            this.lblLecturaLote.Size = new System.Drawing.Size(701, 31);
            this.lblLecturaLote.TabIndex = 34;
            this.lblLecturaLote.Text = "Lectura Lote :";
            this.lblLecturaLote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmLotesSNK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 429);
            this.Controls.Add(this.FrameLotes);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmLotesSNK";
            this.Text = "Lectura de códigos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLotesSNK_FormClosing);
            this.Load += new System.EventHandler(this.FrmLotesSNK_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLotesSNK_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameLotes.ResumeLayout(false);
            this.gpoUbicaciones.ResumeLayout(false);
            this.gpoUbicaciones.PerformLayout();
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
        private System.Windows.Forms.Label lblLecturaLote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader Existencia;
        private System.Windows.Forms.GroupBox gpoUbicaciones;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label lblEntrepaño;
        private SC_ControlsCS.scTextBoxExt txtEntrepaño;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblEstante;
        private SC_ControlsCS.scTextBoxExt txtEstante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPasillo;
        private SC_ControlsCS.scTextBoxExt txtPasillo;
        private System.Windows.Forms.Label label6;
    }
}