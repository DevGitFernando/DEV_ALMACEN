namespace Farmacia.Turnos
{
    partial class FrmTurnos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTurnos));
            this.FramePedidos = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cboTipoConsultas = new SC_ControlsCS.scComboBoxExt();
            this.txtApMaterno = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtApPaterno = new SC_ControlsCS.scTextBoxExt();
            this.txtIdPaciente = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerar = new System.Windows.Forms.ToolStripButton();
            this.txtEdad = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.FramePedidos.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FramePedidos
            // 
            this.FramePedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FramePedidos.Controls.Add(this.txtEdad);
            this.FramePedidos.Controls.Add(this.label5);
            this.FramePedidos.Controls.Add(this.label15);
            this.FramePedidos.Controls.Add(this.cboTipoConsultas);
            this.FramePedidos.Controls.Add(this.txtApMaterno);
            this.FramePedidos.Controls.Add(this.label4);
            this.FramePedidos.Controls.Add(this.txtApPaterno);
            this.FramePedidos.Controls.Add(this.txtIdPaciente);
            this.FramePedidos.Controls.Add(this.label3);
            this.FramePedidos.Controls.Add(this.label1);
            this.FramePedidos.Controls.Add(this.txtNombre);
            this.FramePedidos.Controls.Add(this.label2);
            this.FramePedidos.Location = new System.Drawing.Point(11, 74);
            this.FramePedidos.Margin = new System.Windows.Forms.Padding(4);
            this.FramePedidos.Name = "FramePedidos";
            this.FramePedidos.Padding = new System.Windows.Forms.Padding(4);
            this.FramePedidos.Size = new System.Drawing.Size(518, 214);
            this.FramePedidos.TabIndex = 2;
            this.FramePedidos.TabStop = false;
            this.FramePedidos.Text = "Información";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(23, 39);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(122, 16);
            this.label15.TabIndex = 64;
            this.label15.Text = "Tipo Consulta :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoConsultas
            // 
            this.cboTipoConsultas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTipoConsultas.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoConsultas.Data = "";
            this.cboTipoConsultas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoConsultas.Filtro = " 1 = 1";
            this.cboTipoConsultas.FormattingEnabled = true;
            this.cboTipoConsultas.ListaItemsBusqueda = 20;
            this.cboTipoConsultas.Location = new System.Drawing.Point(154, 36);
            this.cboTipoConsultas.Margin = new System.Windows.Forms.Padding(4);
            this.cboTipoConsultas.MostrarToolTip = false;
            this.cboTipoConsultas.Name = "cboTipoConsultas";
            this.cboTipoConsultas.Size = new System.Drawing.Size(291, 24);
            this.cboTipoConsultas.TabIndex = 0;
            // 
            // txtApMaterno
            // 
            this.txtApMaterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApMaterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtApMaterno.Decimales = 2;
            this.txtApMaterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtApMaterno.ForeColor = System.Drawing.Color.Black;
            this.txtApMaterno.Location = new System.Drawing.Point(154, 160);
            this.txtApMaterno.Margin = new System.Windows.Forms.Padding(4);
            this.txtApMaterno.MaxLength = 50;
            this.txtApMaterno.Name = "txtApMaterno";
            this.txtApMaterno.PermitirApostrofo = false;
            this.txtApMaterno.PermitirNegativos = false;
            this.txtApMaterno.Size = new System.Drawing.Size(291, 22);
            this.txtApMaterno.TabIndex = 5;
            this.txtApMaterno.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(55, 164);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 18);
            this.label4.TabIndex = 15;
            this.label4.Text = "Ap. Materno :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtApPaterno
            // 
            this.txtApPaterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApPaterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtApPaterno.Decimales = 2;
            this.txtApPaterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtApPaterno.ForeColor = System.Drawing.Color.Black;
            this.txtApPaterno.Location = new System.Drawing.Point(154, 130);
            this.txtApPaterno.Margin = new System.Windows.Forms.Padding(4);
            this.txtApPaterno.MaxLength = 50;
            this.txtApPaterno.Name = "txtApPaterno";
            this.txtApPaterno.PermitirApostrofo = false;
            this.txtApPaterno.PermitirNegativos = false;
            this.txtApPaterno.Size = new System.Drawing.Size(291, 22);
            this.txtApPaterno.TabIndex = 4;
            this.txtApPaterno.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtIdPaciente
            // 
            this.txtIdPaciente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPaciente.Decimales = 2;
            this.txtIdPaciente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPaciente.ForeColor = System.Drawing.Color.Black;
            this.txtIdPaciente.Location = new System.Drawing.Point(154, 68);
            this.txtIdPaciente.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdPaciente.MaxLength = 4;
            this.txtIdPaciente.Name = "txtIdPaciente";
            this.txtIdPaciente.PermitirApostrofo = false;
            this.txtIdPaciente.PermitirNegativos = false;
            this.txtIdPaciente.Size = new System.Drawing.Size(103, 22);
            this.txtIdPaciente.TabIndex = 1;
            this.txtIdPaciente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(55, 134);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 18);
            this.label3.TabIndex = 15;
            this.label3.Text = "Ap. Paterno :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(51, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "Id. Paciente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 2;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(154, 98);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(291, 22);
            this.txtNombre.TabIndex = 3;
            this.txtNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(74, 102);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "Nombre :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGenerar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(542, 65);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(54, 62);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 2);
            // 
            // btnGenerar
            // 
            this.btnGenerar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerar.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerar.Image")));
            this.btnGenerar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(54, 62);
            this.btnGenerar.Text = "Generar Turno";
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // txtEdad
            // 
            this.txtEdad.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEdad.Decimales = 2;
            this.txtEdad.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEdad.ForeColor = System.Drawing.Color.Black;
            this.txtEdad.Location = new System.Drawing.Point(342, 68);
            this.txtEdad.Margin = new System.Windows.Forms.Padding(4);
            this.txtEdad.MaxLength = 4;
            this.txtEdad.Name = "txtEdad";
            this.txtEdad.PermitirApostrofo = false;
            this.txtEdad.PermitirNegativos = false;
            this.txtEdad.Size = new System.Drawing.Size(103, 22);
            this.txtEdad.TabIndex = 2;
            this.txtEdad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(281, 72);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 18);
            this.label5.TabIndex = 66;
            this.label5.Text = "Edad :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmTurnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 301);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FramePedidos);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmTurnos";
            this.ShowIcon = false;
            this.Text = "Turnos Consultas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListaDeSurtidosPedido_Load);
            this.FramePedidos.ResumeLayout(false);
            this.FramePedidos.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FramePedidos;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGenerar;
        private SC_ControlsCS.scTextBoxExt txtIdPaciente;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtApPaterno;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtApMaterno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label15;
        private SC_ControlsCS.scComboBoxExt cboTipoConsultas;
        private SC_ControlsCS.scTextBoxExt txtEdad;
        private System.Windows.Forms.Label label5;
    }
}