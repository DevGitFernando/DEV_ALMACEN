namespace Dll_SII_INadro.Configuracion
{
    partial class FrmFirmasParaValidaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFirmasParaValidaciones));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameInformacion = new System.Windows.Forms.GroupBox();
            this.txtNombre_Director = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt7 = new SC_ControlsCS.scLabelExt();
            this.txtNombre_Administrador = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt6 = new SC_ControlsCS.scLabelExt();
            this.txtNombre_PersonalFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt5 = new SC_ControlsCS.scLabelExt();
            this.lblFarmacia = new SC_ControlsCS.scLabelExt();
            this.txtIdFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.lblEstado = new SC_ControlsCS.scLabelExt();
            this.txtIdEstado = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameInformacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(701, 25);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameInformacion
            // 
            this.FrameInformacion.Controls.Add(this.txtNombre_Director);
            this.FrameInformacion.Controls.Add(this.scLabelExt7);
            this.FrameInformacion.Controls.Add(this.txtNombre_Administrador);
            this.FrameInformacion.Controls.Add(this.scLabelExt6);
            this.FrameInformacion.Controls.Add(this.txtNombre_PersonalFarmacia);
            this.FrameInformacion.Controls.Add(this.scLabelExt5);
            this.FrameInformacion.Controls.Add(this.lblFarmacia);
            this.FrameInformacion.Controls.Add(this.txtIdFarmacia);
            this.FrameInformacion.Controls.Add(this.scLabelExt4);
            this.FrameInformacion.Controls.Add(this.lblEstado);
            this.FrameInformacion.Controls.Add(this.txtIdEstado);
            this.FrameInformacion.Controls.Add(this.scLabelExt1);
            this.FrameInformacion.Location = new System.Drawing.Point(8, 28);
            this.FrameInformacion.Name = "FrameInformacion";
            this.FrameInformacion.Size = new System.Drawing.Size(686, 147);
            this.FrameInformacion.TabIndex = 1;
            this.FrameInformacion.TabStop = false;
            this.FrameInformacion.Text = "Datos de firma";
            // 
            // txtNombre_Director
            // 
            this.txtNombre_Director.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre_Director.Decimales = 2;
            this.txtNombre_Director.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre_Director.ForeColor = System.Drawing.Color.Black;
            this.txtNombre_Director.Location = new System.Drawing.Point(141, 117);
            this.txtNombre_Director.MaxLength = 100;
            this.txtNombre_Director.Name = "txtNombre_Director";
            this.txtNombre_Director.PermitirApostrofo = false;
            this.txtNombre_Director.PermitirNegativos = false;
            this.txtNombre_Director.Size = new System.Drawing.Size(534, 20);
            this.txtNombre_Director.TabIndex = 4;
            // 
            // scLabelExt7
            // 
            this.scLabelExt7.Location = new System.Drawing.Point(18, 117);
            this.scLabelExt7.MostrarToolTip = false;
            this.scLabelExt7.Name = "scLabelExt7";
            this.scLabelExt7.Size = new System.Drawing.Size(120, 20);
            this.scLabelExt7.TabIndex = 10;
            this.scLabelExt7.Text = "Nombre director : ";
            this.scLabelExt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNombre_Administrador
            // 
            this.txtNombre_Administrador.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre_Administrador.Decimales = 2;
            this.txtNombre_Administrador.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre_Administrador.ForeColor = System.Drawing.Color.Black;
            this.txtNombre_Administrador.Location = new System.Drawing.Point(141, 93);
            this.txtNombre_Administrador.MaxLength = 100;
            this.txtNombre_Administrador.Name = "txtNombre_Administrador";
            this.txtNombre_Administrador.PermitirApostrofo = false;
            this.txtNombre_Administrador.PermitirNegativos = false;
            this.txtNombre_Administrador.Size = new System.Drawing.Size(534, 20);
            this.txtNombre_Administrador.TabIndex = 3;
            // 
            // scLabelExt6
            // 
            this.scLabelExt6.Location = new System.Drawing.Point(18, 93);
            this.scLabelExt6.MostrarToolTip = true;
            this.scLabelExt6.Name = "scLabelExt6";
            this.scLabelExt6.Size = new System.Drawing.Size(120, 20);
            this.scLabelExt6.TabIndex = 8;
            this.scLabelExt6.Text = "Nombre administrador : ";
            this.scLabelExt6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNombre_PersonalFarmacia
            // 
            this.txtNombre_PersonalFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre_PersonalFarmacia.Decimales = 2;
            this.txtNombre_PersonalFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre_PersonalFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtNombre_PersonalFarmacia.Location = new System.Drawing.Point(141, 68);
            this.txtNombre_PersonalFarmacia.MaxLength = 100;
            this.txtNombre_PersonalFarmacia.Name = "txtNombre_PersonalFarmacia";
            this.txtNombre_PersonalFarmacia.PermitirApostrofo = false;
            this.txtNombre_PersonalFarmacia.PermitirNegativos = false;
            this.txtNombre_PersonalFarmacia.Size = new System.Drawing.Size(534, 20);
            this.txtNombre_PersonalFarmacia.TabIndex = 2;
            // 
            // scLabelExt5
            // 
            this.scLabelExt5.Location = new System.Drawing.Point(18, 68);
            this.scLabelExt5.MostrarToolTip = false;
            this.scLabelExt5.Name = "scLabelExt5";
            this.scLabelExt5.Size = new System.Drawing.Size(120, 20);
            this.scLabelExt5.TabIndex = 6;
            this.scLabelExt5.Text = "Personal de farmacia : ";
            this.scLabelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(209, 43);
            this.lblFarmacia.MostrarToolTip = false;
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(466, 20);
            this.lblFarmacia.TabIndex = 5;
            this.lblFarmacia.Text = "scLabelExt3";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdFarmacia
            // 
            this.txtIdFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFarmacia.Decimales = 2;
            this.txtIdFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtIdFarmacia.Location = new System.Drawing.Point(141, 43);
            this.txtIdFarmacia.MaxLength = 4;
            this.txtIdFarmacia.Name = "txtIdFarmacia";
            this.txtIdFarmacia.PermitirApostrofo = false;
            this.txtIdFarmacia.PermitirNegativos = false;
            this.txtIdFarmacia.Size = new System.Drawing.Size(64, 20);
            this.txtIdFarmacia.TabIndex = 1;
            this.txtIdFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFarmacia.TextChanged += new System.EventHandler(this.txtIdFarmacia_TextChanged);
            this.txtIdFarmacia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdFarmacia_KeyDown);
            this.txtIdFarmacia.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFarmacia_Validating);
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Location = new System.Drawing.Point(18, 43);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(120, 20);
            this.scLabelExt4.TabIndex = 3;
            this.scLabelExt4.Text = "Farmacia : ";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstado.Location = new System.Drawing.Point(209, 19);
            this.lblEstado.MostrarToolTip = false;
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(466, 20);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "scLabelExt2";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdEstado
            // 
            this.txtIdEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdEstado.Decimales = 2;
            this.txtIdEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdEstado.ForeColor = System.Drawing.Color.Black;
            this.txtIdEstado.Location = new System.Drawing.Point(141, 19);
            this.txtIdEstado.MaxLength = 2;
            this.txtIdEstado.Name = "txtIdEstado";
            this.txtIdEstado.PermitirApostrofo = false;
            this.txtIdEstado.PermitirNegativos = false;
            this.txtIdEstado.Size = new System.Drawing.Size(64, 20);
            this.txtIdEstado.TabIndex = 0;
            this.txtIdEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdEstado.TextChanged += new System.EventHandler(this.txtIdEstado_TextChanged);
            this.txtIdEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdEstado_KeyDown);
            this.txtIdEstado.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdEstado_Validating);
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(18, 19);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(120, 20);
            this.scLabelExt1.TabIndex = 0;
            this.scLabelExt1.Text = "Estado : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmFirmasParaValidaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 183);
            this.Controls.Add(this.FrameInformacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmFirmasParaValidaciones";
            this.Text = "Firmas autorizadadas para validaciónes";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFirmasParaValidaciones_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameInformacion.ResumeLayout(false);
            this.FrameInformacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameInformacion;
        private SC_ControlsCS.scLabelExt lblEstado;
        private SC_ControlsCS.scTextBoxExt txtIdEstado;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scLabelExt lblFarmacia;
        private SC_ControlsCS.scTextBoxExt txtIdFarmacia;
        private SC_ControlsCS.scLabelExt scLabelExt4;
        private SC_ControlsCS.scTextBoxExt txtNombre_PersonalFarmacia;
        private SC_ControlsCS.scLabelExt scLabelExt5;
        private SC_ControlsCS.scTextBoxExt txtNombre_Director;
        private SC_ControlsCS.scLabelExt scLabelExt7;
        private SC_ControlsCS.scTextBoxExt txtNombre_Administrador;
        private SC_ControlsCS.scLabelExt scLabelExt6;
    }
}