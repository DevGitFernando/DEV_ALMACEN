namespace OficinaCentral.Consultas
{
    partial class FrmProductosEnFarmacias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductosEnFarmacias));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameProducto = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCodigoEAN = new SC_ControlsCS.scTextBoxExt();
            this.txtClaveInternaSal = new SC_ControlsCS.scTextBoxExt();
            this.label14 = new System.Windows.Forms.Label();
            this.lblDescripcionSal = new System.Windows.Forms.Label();
            this.lblClaveSal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.txtDescripcion = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameProducto.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(756, 25);
            this.toolStripBarraMenu.TabIndex = 10;
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
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameProducto
            // 
            this.FrameProducto.Controls.Add(this.label13);
            this.FrameProducto.Controls.Add(this.txtCodigoEAN);
            this.FrameProducto.Controls.Add(this.txtClaveInternaSal);
            this.FrameProducto.Controls.Add(this.label14);
            this.FrameProducto.Controls.Add(this.lblDescripcionSal);
            this.FrameProducto.Controls.Add(this.lblClaveSal);
            this.FrameProducto.Controls.Add(this.label3);
            this.FrameProducto.Controls.Add(this.txtId);
            this.FrameProducto.Controls.Add(this.txtDescripcion);
            this.FrameProducto.Controls.Add(this.label1);
            this.FrameProducto.Controls.Add(this.label2);
            this.FrameProducto.Location = new System.Drawing.Point(12, 28);
            this.FrameProducto.Name = "FrameProducto";
            this.FrameProducto.Size = new System.Drawing.Size(502, 172);
            this.FrameProducto.TabIndex = 11;
            this.FrameProducto.TabStop = false;
            this.FrameProducto.Text = "Datos Producto :";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(295, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "EAN :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodigoEAN
            // 
            this.txtCodigoEAN.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoEAN.Decimales = 2;
            this.txtCodigoEAN.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoEAN.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoEAN.Location = new System.Drawing.Point(343, 19);
            this.txtCodigoEAN.MaxLength = 15;
            this.txtCodigoEAN.Name = "txtCodigoEAN";
            this.txtCodigoEAN.PermitirApostrofo = false;
            this.txtCodigoEAN.PermitirNegativos = false;
            this.txtCodigoEAN.Size = new System.Drawing.Size(148, 20);
            this.txtCodigoEAN.TabIndex = 1;
            this.txtCodigoEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtClaveInternaSal
            // 
            this.txtClaveInternaSal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveInternaSal.Decimales = 2;
            this.txtClaveInternaSal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveInternaSal.ForeColor = System.Drawing.Color.Black;
            this.txtClaveInternaSal.Location = new System.Drawing.Point(114, 44);
            this.txtClaveInternaSal.MaxLength = 4;
            this.txtClaveInternaSal.Name = "txtClaveInternaSal";
            this.txtClaveInternaSal.PermitirApostrofo = false;
            this.txtClaveInternaSal.PermitirNegativos = false;
            this.txtClaveInternaSal.Size = new System.Drawing.Size(82, 20);
            this.txtClaveInternaSal.TabIndex = 2;
            this.txtClaveInternaSal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(9, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(102, 21);
            this.label14.TabIndex = 28;
            this.label14.Text = "Clave Interna SSA :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcionSal
            // 
            this.lblDescripcionSal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionSal.Location = new System.Drawing.Point(114, 68);
            this.lblDescripcionSal.Name = "lblDescripcionSal";
            this.lblDescripcionSal.Size = new System.Drawing.Size(375, 68);
            this.lblDescripcionSal.TabIndex = 5;
            this.lblDescripcionSal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClaveSal
            // 
            this.lblClaveSal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSal.Location = new System.Drawing.Point(343, 44);
            this.lblClaveSal.Name = "lblClaveSal";
            this.lblClaveSal.Size = new System.Drawing.Size(148, 20);
            this.lblClaveSal.TabIndex = 4;
            this.lblClaveSal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(273, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Clave SSA :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(114, 19);
            this.txtId.MaxLength = 8;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(82, 20);
            this.txtId.TabIndex = 0;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDescripcion.Decimales = 2;
            this.txtDescripcion.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDescripcion.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Location = new System.Drawing.Point(114, 139);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.PermitirApostrofo = false;
            this.txtDescripcion.PermitirNegativos = false;
            this.txtDescripcion.Size = new System.Drawing.Size(375, 20);
            this.txtDescripcion.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(28, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Código Interno :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(42, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Descripción :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmProductosEnFarmacias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 458);
            this.Controls.Add(this.FrameProducto);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmProductosEnFarmacias";
            this.Text = "Localizar Productos en Farmacias";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameProducto.ResumeLayout(false);
            this.FrameProducto.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameProducto;
        private System.Windows.Forms.Label label13;
        private SC_ControlsCS.scTextBoxExt txtCodigoEAN;
        private SC_ControlsCS.scTextBoxExt txtClaveInternaSal;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblDescripcionSal;
        private System.Windows.Forms.Label lblClaveSal;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtId;
        private SC_ControlsCS.scTextBoxExt txtDescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}