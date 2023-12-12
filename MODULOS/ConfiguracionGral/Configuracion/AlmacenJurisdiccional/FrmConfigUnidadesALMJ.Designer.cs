namespace Configuracion.Configuracion
{
    partial class FrmConfigUnidadesALMJ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigUnidadesALMJ));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameCSGN = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboEmpresaCSGN = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.cboFarmaciaCSGN = new SC_ControlsCS.scComboBoxExt();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstadoCSGN = new SC_ControlsCS.scComboBoxExt();
            this.FrameVenta = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEmpresa = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboFarmacia = new SC_ControlsCS.scComboBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.cboEstado = new SC_ControlsCS.scComboBoxExt();
            this.tmRevisar = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameCSGN.SuspendLayout();
            this.FrameVenta.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(350, 25);
            this.toolStripBarraMenu.TabIndex = 9;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameCSGN
            // 
            this.FrameCSGN.Controls.Add(this.label2);
            this.FrameCSGN.Controls.Add(this.cboEmpresaCSGN);
            this.FrameCSGN.Controls.Add(this.label1);
            this.FrameCSGN.Controls.Add(this.cboFarmaciaCSGN);
            this.FrameCSGN.Controls.Add(this.lblEstado);
            this.FrameCSGN.Controls.Add(this.cboEstadoCSGN);
            this.FrameCSGN.Location = new System.Drawing.Point(12, 28);
            this.FrameCSGN.Name = "FrameCSGN";
            this.FrameCSGN.Size = new System.Drawing.Size(326, 107);
            this.FrameCSGN.TabIndex = 0;
            this.FrameCSGN.TabStop = false;
            this.FrameCSGN.Text = "Unidad de Consginación";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 29;
            this.label2.Text = "Empresa :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresaCSGN
            // 
            this.cboEmpresaCSGN.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresaCSGN.Data = "";
            this.cboEmpresaCSGN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresaCSGN.Location = new System.Drawing.Point(75, 18);
            this.cboEmpresaCSGN.Name = "cboEmpresaCSGN";
            this.cboEmpresaCSGN.Size = new System.Drawing.Size(235, 21);
            this.cboEmpresaCSGN.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 27;
            this.label1.Text = "Farmacia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmaciaCSGN
            // 
            this.cboFarmaciaCSGN.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmaciaCSGN.Data = "";
            this.cboFarmaciaCSGN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmaciaCSGN.Location = new System.Drawing.Point(75, 72);
            this.cboFarmaciaCSGN.Name = "cboFarmaciaCSGN";
            this.cboFarmaciaCSGN.Size = new System.Drawing.Size(235, 21);
            this.cboFarmaciaCSGN.TabIndex = 2;
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(16, 46);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(56, 17);
            this.lblEstado.TabIndex = 25;
            this.lblEstado.Text = "Estado :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstadoCSGN
            // 
            this.cboEstadoCSGN.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstadoCSGN.Data = "";
            this.cboEstadoCSGN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadoCSGN.Location = new System.Drawing.Point(75, 45);
            this.cboEstadoCSGN.Name = "cboEstadoCSGN";
            this.cboEstadoCSGN.Size = new System.Drawing.Size(235, 21);
            this.cboEstadoCSGN.TabIndex = 1;
            // 
            // FrameVenta
            // 
            this.FrameVenta.Controls.Add(this.label3);
            this.FrameVenta.Controls.Add(this.cboEmpresa);
            this.FrameVenta.Controls.Add(this.label4);
            this.FrameVenta.Controls.Add(this.cboFarmacia);
            this.FrameVenta.Controls.Add(this.label5);
            this.FrameVenta.Controls.Add(this.cboEstado);
            this.FrameVenta.Location = new System.Drawing.Point(12, 141);
            this.FrameVenta.Name = "FrameVenta";
            this.FrameVenta.Size = new System.Drawing.Size(326, 107);
            this.FrameVenta.TabIndex = 1;
            this.FrameVenta.TabStop = false;
            this.FrameVenta.Text = "Unidad de Venta";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "Empresa :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresa
            // 
            this.cboEmpresa.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresa.Data = "";
            this.cboEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresa.Location = new System.Drawing.Point(75, 18);
            this.cboEmpresa.Name = "cboEmpresa";
            this.cboEmpresa.Size = new System.Drawing.Size(235, 21);
            this.cboEmpresa.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "Farmacia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacia
            // 
            this.cboFarmacia.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacia.Data = "";
            this.cboFarmacia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacia.Location = new System.Drawing.Point(75, 72);
            this.cboFarmacia.Name = "cboFarmacia";
            this.cboFarmacia.Size = new System.Drawing.Size(235, 21);
            this.cboFarmacia.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 25;
            this.label5.Text = "Estado :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstado
            // 
            this.cboEstado.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstado.Data = "";
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Location = new System.Drawing.Point(75, 45);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(235, 21);
            this.cboEstado.TabIndex = 1;
            // 
            // tmRevisar
            // 
            this.tmRevisar.Interval = 400;
            this.tmRevisar.Tick += new System.EventHandler(this.tmRevisar_Tick);
            // 
            // FrmConfigUnidadesALMJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 258);
            this.Controls.Add(this.FrameVenta);
            this.Controls.Add(this.FrameCSGN);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmConfigUnidadesALMJ";
            this.Text = "Configuración de Unidades para Almancen Jurisdiccional";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConfigUnidadesALMJ_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameCSGN.ResumeLayout(false);
            this.FrameVenta.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameCSGN;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboFarmaciaCSGN;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstadoCSGN;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboEmpresaCSGN;
        private System.Windows.Forms.GroupBox FrameVenta;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboEmpresa;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboFarmacia;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scComboBoxExt cboEstado;
        private System.Windows.Forms.Timer tmRevisar;
    }
}