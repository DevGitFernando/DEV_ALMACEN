namespace DllFarmaciaSoft.Ayudas
{
    partial class FrmHelpBeneficiarios
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
            this.FramaDatos = new System.Windows.Forms.GroupBox();
            this.txtCURP = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.chkFechaNacimiento = new System.Windows.Forms.CheckBox();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cboSexo = new SC_ControlsCS.scComboBoxExt();
            this.txtMaterno = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPaterno = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameBeneficiarios = new System.Windows.Forms.GroupBox();
            this.lblProcesando = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lvBeneficiarios = new System.Windows.Forms.ListView();
            this.contexMenuBeneficiarios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.integrerarInformaciónDeBeneficiarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.chkImportarBeneficiarios = new System.Windows.Forms.CheckBox();
            this.lblVigencia = new System.Windows.Forms.Label();
            this.cboVigencia = new SC_ControlsCS.scComboBoxExt();
            this.FramaDatos.SuspendLayout();
            this.FrameBeneficiarios.SuspendLayout();
            this.contexMenuBeneficiarios.SuspendLayout();
            this.SuspendLayout();
            // 
            // FramaDatos
            // 
            this.FramaDatos.Controls.Add(this.txtCURP);
            this.FramaDatos.Controls.Add(this.label3);
            this.FramaDatos.Controls.Add(this.txtFolio);
            this.FramaDatos.Controls.Add(this.label1);
            this.FramaDatos.Controls.Add(this.chkFechaNacimiento);
            this.FramaDatos.Controls.Add(this.dtpFechaNacimiento);
            this.FramaDatos.Controls.Add(this.btnBuscar);
            this.FramaDatos.Controls.Add(this.label8);
            this.FramaDatos.Controls.Add(this.cboSexo);
            this.FramaDatos.Controls.Add(this.txtMaterno);
            this.FramaDatos.Controls.Add(this.label4);
            this.FramaDatos.Controls.Add(this.txtPaterno);
            this.FramaDatos.Controls.Add(this.label6);
            this.FramaDatos.Controls.Add(this.txtNombre);
            this.FramaDatos.Controls.Add(this.label2);
            this.FramaDatos.Location = new System.Drawing.Point(8, 5);
            this.FramaDatos.Name = "FramaDatos";
            this.FramaDatos.Size = new System.Drawing.Size(1046, 85);
            this.FramaDatos.TabIndex = 0;
            this.FramaDatos.TabStop = false;
            this.FramaDatos.Text = "Datos Personales Beneficiario:";
            // 
            // txtCURP
            // 
            this.txtCURP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCURP.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCURP.Decimales = 2;
            this.txtCURP.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCURP.ForeColor = System.Drawing.Color.Black;
            this.txtCURP.Location = new System.Drawing.Point(788, 32);
            this.txtCURP.MaxLength = 18;
            this.txtCURP.Name = "txtCURP";
            this.txtCURP.PermitirApostrofo = false;
            this.txtCURP.PermitirNegativos = false;
            this.txtCURP.Size = new System.Drawing.Size(248, 20);
            this.txtCURP.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(789, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "CURP :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFolio
            // 
            this.txtFolio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(47, 58);
            this.txtFolio.MaxLength = 20;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(151, 20);
            this.txtFolio.TabIndex = 4;
            this.txtFolio.Text = "12345678901234567890";
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkFechaNacimiento
            // 
            this.chkFechaNacimiento.Location = new System.Drawing.Point(220, 56);
            this.chkFechaNacimiento.Name = "chkFechaNacimiento";
            this.chkFechaNacimiento.Size = new System.Drawing.Size(143, 24);
            this.chkFechaNacimiento.TabIndex = 5;
            this.chkFechaNacimiento.Text = "Usar Fecha Nacimiento :";
            this.chkFechaNacimiento.UseVisualStyleBackColor = true;
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(363, 58);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(105, 20);
            this.dtpFechaNacimiento.TabIndex = 6;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(911, 56);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(126, 21);
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(490, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Sexo :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSexo
            // 
            this.cboSexo.BackColorEnabled = System.Drawing.Color.White;
            this.cboSexo.Data = "";
            this.cboSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSexo.Filtro = " 1 = 1";
            this.cboSexo.FormattingEnabled = true;
            this.cboSexo.ListaItemsBusqueda = 20;
            this.cboSexo.Location = new System.Drawing.Point(529, 58);
            this.cboSexo.MostrarToolTip = false;
            this.cboSexo.Name = "cboSexo";
            this.cboSexo.Size = new System.Drawing.Size(77, 21);
            this.cboSexo.TabIndex = 7;
            // 
            // txtMaterno
            // 
            this.txtMaterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtMaterno.Decimales = 2;
            this.txtMaterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtMaterno.ForeColor = System.Drawing.Color.Black;
            this.txtMaterno.Location = new System.Drawing.Point(269, 32);
            this.txtMaterno.MaxLength = 50;
            this.txtMaterno.Name = "txtMaterno";
            this.txtMaterno.PermitirApostrofo = false;
            this.txtMaterno.PermitirNegativos = false;
            this.txtMaterno.Size = new System.Drawing.Size(248, 20);
            this.txtMaterno.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(269, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Apellido Materno :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPaterno
            // 
            this.txtPaterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPaterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPaterno.Decimales = 2;
            this.txtPaterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPaterno.ForeColor = System.Drawing.Color.Black;
            this.txtPaterno.Location = new System.Drawing.Point(8, 32);
            this.txtPaterno.MaxLength = 50;
            this.txtPaterno.Name = "txtPaterno";
            this.txtPaterno.PermitirApostrofo = false;
            this.txtPaterno.PermitirNegativos = false;
            this.txtPaterno.Size = new System.Drawing.Size(248, 20);
            this.txtPaterno.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Apellido Paterno :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 2;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(529, 32);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(248, 20);
            this.txtNombre.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(530, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameBeneficiarios
            // 
            this.FrameBeneficiarios.Controls.Add(this.lblProcesando);
            this.FrameBeneficiarios.Controls.Add(this.progressBar);
            this.FrameBeneficiarios.Controls.Add(this.lvBeneficiarios);
            this.FrameBeneficiarios.Location = new System.Drawing.Point(8, 93);
            this.FrameBeneficiarios.Name = "FrameBeneficiarios";
            this.FrameBeneficiarios.Size = new System.Drawing.Size(1046, 368);
            this.FrameBeneficiarios.TabIndex = 1;
            this.FrameBeneficiarios.TabStop = false;
            this.FrameBeneficiarios.Text = "Beneficiarios Encontrados";
            // 
            // lblProcesando
            // 
            this.lblProcesando.BackColor = System.Drawing.Color.White;
            this.lblProcesando.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcesando.Location = new System.Drawing.Point(213, 131);
            this.lblProcesando.Name = "lblProcesando";
            this.lblProcesando.Size = new System.Drawing.Size(609, 23);
            this.lblProcesando.TabIndex = 1;
            this.lblProcesando.Text = "Solicitando información";
            this.lblProcesando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.progressBar.Location = new System.Drawing.Point(216, 157);
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(606, 19);
            this.progressBar.Step = 50;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 2;
            // 
            // lvBeneficiarios
            // 
            this.lvBeneficiarios.ContextMenuStrip = this.contexMenuBeneficiarios;
            this.lvBeneficiarios.Location = new System.Drawing.Point(10, 19);
            this.lvBeneficiarios.Name = "lvBeneficiarios";
            this.lvBeneficiarios.Size = new System.Drawing.Size(1026, 338);
            this.lvBeneficiarios.TabIndex = 0;
            this.lvBeneficiarios.UseCompatibleStateImageBehavior = false;
            this.lvBeneficiarios.DoubleClick += new System.EventHandler(this.lvBeneficiarios_DoubleClick);
            this.lvBeneficiarios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvBeneficiarios_KeyDown);
            // 
            // contexMenuBeneficiarios
            // 
            this.contexMenuBeneficiarios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.integrerarInformaciónDeBeneficiarioToolStripMenuItem});
            this.contexMenuBeneficiarios.Name = "contexMenuBeneficiarios";
            this.contexMenuBeneficiarios.Size = new System.Drawing.Size(275, 26);
            // 
            // integrerarInformaciónDeBeneficiarioToolStripMenuItem
            // 
            this.integrerarInformaciónDeBeneficiarioToolStripMenuItem.Name = "integrerarInformaciónDeBeneficiarioToolStripMenuItem";
            this.integrerarInformaciónDeBeneficiarioToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.integrerarInformaciónDeBeneficiarioToolStripMenuItem.Text = "Integrerar información de Beneficiario";
            this.integrerarInformaciónDeBeneficiarioToolStripMenuItem.Click += new System.EventHandler(this.integrerarInformaciónDeBeneficiarioToolStripMenuItem_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(932, 463);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(113, 25);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(813, 463);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(113, 25);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // chkImportarBeneficiarios
            // 
            this.chkImportarBeneficiarios.Location = new System.Drawing.Point(18, 470);
            this.chkImportarBeneficiarios.Name = "chkImportarBeneficiarios";
            this.chkImportarBeneficiarios.Size = new System.Drawing.Size(176, 17);
            this.chkImportarBeneficiarios.TabIndex = 2;
            this.chkImportarBeneficiarios.Text = "Buscar Beneficiarios en Central";
            this.chkImportarBeneficiarios.UseVisualStyleBackColor = true;
            // 
            // lblVigencia
            // 
            this.lblVigencia.Location = new System.Drawing.Point(203, 471);
            this.lblVigencia.Name = "lblVigencia";
            this.lblVigencia.Size = new System.Drawing.Size(52, 13);
            this.lblVigencia.TabIndex = 19;
            this.lblVigencia.Text = "Vigente :";
            this.lblVigencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboVigencia
            // 
            this.cboVigencia.BackColorEnabled = System.Drawing.Color.White;
            this.cboVigencia.Data = "";
            this.cboVigencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVigencia.Filtro = " 1 = 1";
            this.cboVigencia.FormattingEnabled = true;
            this.cboVigencia.ListaItemsBusqueda = 20;
            this.cboVigencia.Location = new System.Drawing.Point(256, 467);
            this.cboVigencia.MostrarToolTip = false;
            this.cboVigencia.Name = "cboVigencia";
            this.cboVigencia.Size = new System.Drawing.Size(72, 21);
            this.cboVigencia.TabIndex = 3;
            // 
            // FrmHelpBeneficiarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 500);
            this.Controls.Add(this.lblVigencia);
            this.Controls.Add(this.cboVigencia);
            this.Controls.Add(this.chkImportarBeneficiarios);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.FrameBeneficiarios);
            this.Controls.Add(this.FramaDatos);
            this.Name = "FrmHelpBeneficiarios";
            this.Text = "Catálogo de Beneficiarios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmHelpBeneficiarios_Load);
            this.FramaDatos.ResumeLayout(false);
            this.FramaDatos.PerformLayout();
            this.FrameBeneficiarios.ResumeLayout(false);
            this.contexMenuBeneficiarios.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FramaDatos;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scComboBoxExt cboSexo;
        private SC_ControlsCS.scTextBoxExt txtMaterno;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtPaterno;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FrameBeneficiarios;
        private System.Windows.Forms.ListView lvBeneficiarios;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.CheckBox chkFechaNacimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkImportarBeneficiarios;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProcesando;
        private System.Windows.Forms.Label lblVigencia;
        private SC_ControlsCS.scComboBoxExt cboVigencia;
        private System.Windows.Forms.ContextMenuStrip contexMenuBeneficiarios;
        private System.Windows.Forms.ToolStripMenuItem integrerarInformaciónDeBeneficiarioToolStripMenuItem;
        private SC_ControlsCS.scTextBoxExt txtCURP;
        private System.Windows.Forms.Label label3;
    }
}