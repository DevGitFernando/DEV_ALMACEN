namespace Almacen.Pedidos
{
    partial class FrmSurtidoDePedido
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if(disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSurtidoDePedido));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdicion = new System.Windows.Forms.ToolStripButton();
            this.btnCargarSurtido = new System.Windows.Forms.ToolStripButton();
            this.btnTerminarEdicion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblStatus_Surtido = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro_02_Surtido = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFolio_02_Surtido = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro_01_Pedido = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFolio_01_Pedido = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTerminal = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblPersonal = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblEnEdicion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEdicion,
            this.btnCargarSurtido,
            this.btnTerminarEdicion,
            this.toolStripSeparator2,
            this.btnSalir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripBarraMenu.Size = new System.Drawing.Size(937, 39);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(36, 36);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 39);
            // 
            // btnEdicion
            // 
            this.btnEdicion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdicion.Image = ((System.Drawing.Image)(resources.GetObject("btnEdicion.Image")));
            this.btnEdicion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdicion.Name = "btnEdicion";
            this.btnEdicion.Size = new System.Drawing.Size(36, 36);
            this.btnEdicion.Text = "Reservar orden de surtido";
            this.btnEdicion.Click += new System.EventHandler(this.btnEdicion_Click);
            // 
            // btnCargarSurtido
            // 
            this.btnCargarSurtido.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCargarSurtido.Image = ((System.Drawing.Image)(resources.GetObject("btnCargarSurtido.Image")));
            this.btnCargarSurtido.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCargarSurtido.Name = "btnCargarSurtido";
            this.btnCargarSurtido.Size = new System.Drawing.Size(36, 36);
            this.btnCargarSurtido.Text = "Mostrar surtido";
            this.btnCargarSurtido.Click += new System.EventHandler(this.btnCargarSurtido_Click);
            // 
            // btnTerminarEdicion
            // 
            this.btnTerminarEdicion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTerminarEdicion.Image = ((System.Drawing.Image)(resources.GetObject("btnTerminarEdicion.Image")));
            this.btnTerminarEdicion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTerminarEdicion.Name = "btnTerminarEdicion";
            this.btnTerminarEdicion.Size = new System.Drawing.Size(36, 36);
            this.btnTerminarEdicion.Text = "Liberar reserva orden de surtido";
            this.btnTerminarEdicion.Click += new System.EventHandler(this.btnTerminarEdicion_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(86, 36);
            this.btnSalir.Text = "Salir";
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblStatus_Surtido);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro_02_Surtido);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFolio_02_Surtido);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro_01_Pedido);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFolio_01_Pedido);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 45);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(915, 150);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // lblStatus_Surtido
            // 
            this.lblStatus_Surtido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus_Surtido.Location = new System.Drawing.Point(201, 105);
            this.lblStatus_Surtido.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStatus_Surtido.Name = "lblStatus_Surtido";
            this.lblStatus_Surtido.Size = new System.Drawing.Size(693, 29);
            this.lblStatus_Surtido.TabIndex = 43;
            this.lblStatus_Surtido.Text = "Fecha de registro del surtido :";
            this.lblStatus_Surtido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 105);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 29);
            this.label5.TabIndex = 42;
            this.label5.Text = "Status de surtido :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro_02_Surtido
            // 
            this.dtpFechaRegistro_02_Surtido.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro_02_Surtido.Enabled = false;
            this.dtpFechaRegistro_02_Surtido.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro_02_Surtido.Location = new System.Drawing.Point(738, 70);
            this.dtpFechaRegistro_02_Surtido.Margin = new System.Windows.Forms.Padding(6);
            this.dtpFechaRegistro_02_Surtido.Name = "dtpFechaRegistro_02_Surtido";
            this.dtpFechaRegistro_02_Surtido.Size = new System.Drawing.Size(156, 34);
            this.dtpFechaRegistro_02_Surtido.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(422, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(304, 29);
            this.label2.TabIndex = 41;
            this.label2.Text = "Fecha de registro del surtido :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolio_02_Surtido
            // 
            this.txtFolio_02_Surtido.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio_02_Surtido.Decimales = 2;
            this.txtFolio_02_Surtido.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio_02_Surtido.ForeColor = System.Drawing.Color.Black;
            this.txtFolio_02_Surtido.Location = new System.Drawing.Point(201, 70);
            this.txtFolio_02_Surtido.Margin = new System.Windows.Forms.Padding(6);
            this.txtFolio_02_Surtido.MaxLength = 8;
            this.txtFolio_02_Surtido.Name = "txtFolio_02_Surtido";
            this.txtFolio_02_Surtido.PermitirApostrofo = false;
            this.txtFolio_02_Surtido.PermitirNegativos = false;
            this.txtFolio_02_Surtido.Size = new System.Drawing.Size(180, 34);
            this.txtFolio_02_Surtido.TabIndex = 1;
            this.txtFolio_02_Surtido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio_02_Surtido.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_02_Surtido_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 70);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 29);
            this.label4.TabIndex = 40;
            this.label4.Text = "Folio de surtido :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro_01_Pedido
            // 
            this.dtpFechaRegistro_01_Pedido.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro_01_Pedido.Enabled = false;
            this.dtpFechaRegistro_01_Pedido.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro_01_Pedido.Location = new System.Drawing.Point(738, 30);
            this.dtpFechaRegistro_01_Pedido.Margin = new System.Windows.Forms.Padding(6);
            this.dtpFechaRegistro_01_Pedido.Name = "dtpFechaRegistro_01_Pedido";
            this.dtpFechaRegistro_01_Pedido.Size = new System.Drawing.Size(156, 34);
            this.dtpFechaRegistro_01_Pedido.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(422, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(304, 29);
            this.label3.TabIndex = 37;
            this.label3.Text = "Fecha de registro del pedido :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolio_01_Pedido
            // 
            this.txtFolio_01_Pedido.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio_01_Pedido.Decimales = 2;
            this.txtFolio_01_Pedido.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio_01_Pedido.ForeColor = System.Drawing.Color.Black;
            this.txtFolio_01_Pedido.Location = new System.Drawing.Point(201, 30);
            this.txtFolio_01_Pedido.Margin = new System.Windows.Forms.Padding(6);
            this.txtFolio_01_Pedido.MaxLength = 6;
            this.txtFolio_01_Pedido.Name = "txtFolio_01_Pedido";
            this.txtFolio_01_Pedido.PermitirApostrofo = false;
            this.txtFolio_01_Pedido.PermitirNegativos = false;
            this.txtFolio_01_Pedido.Size = new System.Drawing.Size(180, 34);
            this.txtFolio_01_Pedido.TabIndex = 0;
            this.txtFolio_01_Pedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio_01_Pedido.TextChanged += new System.EventHandler(this.txtFolio_01_Pedido_TextChanged);
            this.txtFolio_01_Pedido.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_01_Pedido_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 29);
            this.label1.TabIndex = 36;
            this.label1.Text = "Folio de pedido :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTerminal);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblPersonal);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.lblEnEdicion);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(10, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(915, 141);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información adicional";
            // 
            // lblTerminal
            // 
            this.lblTerminal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTerminal.Location = new System.Drawing.Point(201, 99);
            this.lblTerminal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTerminal.Name = "lblTerminal";
            this.lblTerminal.Size = new System.Drawing.Size(693, 29);
            this.lblTerminal.TabIndex = 49;
            this.lblTerminal.Text = "Fecha de registro del surtido :";
            this.lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(12, 99);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(183, 29);
            this.label11.TabIndex = 48;
            this.label11.Text = "Terminal :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPersonal
            // 
            this.lblPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonal.Location = new System.Drawing.Point(201, 65);
            this.lblPersonal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Size = new System.Drawing.Size(693, 29);
            this.lblPersonal.TabIndex = 47;
            this.lblPersonal.Text = "Fecha de registro del surtido :";
            this.lblPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(12, 65);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(183, 29);
            this.label9.TabIndex = 46;
            this.label9.Text = "Personal :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEnEdicion
            // 
            this.lblEnEdicion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEnEdicion.Location = new System.Drawing.Point(201, 29);
            this.lblEnEdicion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblEnEdicion.Name = "lblEnEdicion";
            this.lblEnEdicion.Size = new System.Drawing.Size(693, 29);
            this.lblEnEdicion.TabIndex = 45;
            this.lblEnEdicion.Text = "Fecha de registro del surtido :";
            this.lblEnEdicion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEnEdicion.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 29);
            this.label7.TabIndex = 44;
            this.label7.Text = "Editable :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmSurtidoDePedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 344);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FrmSurtidoDePedido";
            this.ShowIcon = false;
            this.Text = "Surtido de pedido";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmSurtidoDePedido_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnCargarSurtido;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro_01_Pedido;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtFolio_01_Pedido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro_02_Surtido;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtFolio_02_Surtido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStatus_Surtido;
        private System.Windows.Forms.ToolStripButton btnTerminarEdicion;
        private System.Windows.Forms.ToolStripButton btnEdicion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblEnEdicion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPersonal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTerminal;
        private System.Windows.Forms.Label label11;
    }
}