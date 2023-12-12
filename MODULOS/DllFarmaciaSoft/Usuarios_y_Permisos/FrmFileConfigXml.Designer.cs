namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    partial class FrmFileConfigXml
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServidor = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWebService = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtPagina = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.cboUsarWebService = new SC_ControlsCS.scComboBoxExt();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usar web service :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServidor
            // 
            this.txtServidor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtServidor.Decimales = 2;
            this.txtServidor.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtServidor.ForeColor = System.Drawing.Color.Black;
            this.txtServidor.Location = new System.Drawing.Point(107, 42);
            this.txtServidor.MaxLength = 50;
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.PermitirApostrofo = false;
            this.txtServidor.PermitirNegativos = false;
            this.txtServidor.Size = new System.Drawing.Size(299, 20);
            this.txtServidor.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Servidor :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtWebService
            // 
            this.txtWebService.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtWebService.Decimales = 2;
            this.txtWebService.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtWebService.ForeColor = System.Drawing.Color.Black;
            this.txtWebService.Location = new System.Drawing.Point(107, 66);
            this.txtWebService.MaxLength = 50;
            this.txtWebService.Name = "txtWebService";
            this.txtWebService.PermitirApostrofo = false;
            this.txtWebService.PermitirNegativos = false;
            this.txtWebService.Size = new System.Drawing.Size(299, 20);
            this.txtWebService.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Web service :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(200, 118);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(100, 24);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(306, 118);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 24);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtPagina
            // 
            this.txtPagina.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPagina.Decimales = 2;
            this.txtPagina.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPagina.ForeColor = System.Drawing.Color.Black;
            this.txtPagina.Location = new System.Drawing.Point(107, 92);
            this.txtPagina.MaxLength = 50;
            this.txtPagina.Name = "txtPagina";
            this.txtPagina.PermitirApostrofo = false;
            this.txtPagina.PermitirNegativos = false;
            this.txtPagina.Size = new System.Drawing.Size(299, 20);
            this.txtPagina.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Página web :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSSL);
            this.groupBox1.Controls.Add(this.cboUsarWebService);
            this.groupBox1.Controls.Add(this.txtPagina);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Controls.Add(this.txtWebService);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtServidor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkSSL
            // 
            this.chkSSL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSSL.Location = new System.Drawing.Point(302, 16);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(104, 18);
            this.chkSSL.TabIndex = 1;
            this.chkSSL.Text = "SSL";
            this.chkSSL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSSL.UseVisualStyleBackColor = true;
            // 
            // cboUsarWebService
            // 
            this.cboUsarWebService.BackColorEnabled = System.Drawing.Color.White;
            this.cboUsarWebService.Data = "";
            this.cboUsarWebService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsarWebService.Filtro = " 1 = 1";
            this.cboUsarWebService.FormattingEnabled = true;
            this.cboUsarWebService.ListaItemsBusqueda = 20;
            this.cboUsarWebService.Location = new System.Drawing.Point(107, 15);
            this.cboUsarWebService.MostrarToolTip = false;
            this.cboUsarWebService.Name = "cboUsarWebService";
            this.cboUsarWebService.Size = new System.Drawing.Size(44, 21);
            this.cboUsarWebService.TabIndex = 0;
            // 
            // FrmFileConfigXml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 166);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmFileConfigXml";
            this.Text = "Configurar conexión";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFileConfigXml_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmFileConfigXml_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtServidor;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtWebService;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private SC_ControlsCS.scTextBoxExt txtPagina;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scComboBoxExt cboUsarWebService;
        private System.Windows.Forms.CheckBox chkSSL;
    }
}