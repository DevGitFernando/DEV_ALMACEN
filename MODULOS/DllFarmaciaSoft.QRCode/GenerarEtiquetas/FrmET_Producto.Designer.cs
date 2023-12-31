﻿namespace DllFarmaciaSoft.QRCode.GenerarEtiquetas
{
    partial class FrmET_Producto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmET_Producto));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.lblLaboratorio = new SC_ControlsCS.scLabelExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblProducto = new SC_ControlsCS.scLabelExt();
            this.txtCodigoEAN = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnImprimir,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(443, 25);
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
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.lblLaboratorio);
            this.FrameDatos.Controls.Add(this.label5);
            this.FrameDatos.Controls.Add(this.lblProducto);
            this.FrameDatos.Controls.Add(this.txtCodigoEAN);
            this.FrameDatos.Controls.Add(this.label4);
            this.FrameDatos.Location = new System.Drawing.Point(12, 31);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(420, 98);
            this.FrameDatos.TabIndex = 2;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Información";
            // 
            // lblLaboratorio
            // 
            this.lblLaboratorio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLaboratorio.Location = new System.Drawing.Point(101, 65);
            this.lblLaboratorio.MostrarToolTip = false;
            this.lblLaboratorio.Name = "lblLaboratorio";
            this.lblLaboratorio.Size = new System.Drawing.Size(308, 23);
            this.lblLaboratorio.TabIndex = 2;
            this.lblLaboratorio.Text = "scLabelExt1";
            this.lblLaboratorio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(25, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 19);
            this.label5.TabIndex = 17;
            this.label5.Text = "Laboratorio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProducto
            // 
            this.lblProducto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProducto.Location = new System.Drawing.Point(101, 42);
            this.lblProducto.MostrarToolTip = false;
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(308, 23);
            this.lblProducto.TabIndex = 1;
            this.lblProducto.Text = "scLabelExt1";
            this.lblProducto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigoEAN
            // 
            this.txtCodigoEAN.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoEAN.Decimales = 2;
            this.txtCodigoEAN.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoEAN.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoEAN.Location = new System.Drawing.Point(101, 19);
            this.txtCodigoEAN.MaxLength = 20;
            this.txtCodigoEAN.Name = "txtCodigoEAN";
            this.txtCodigoEAN.PermitirApostrofo = false;
            this.txtCodigoEAN.PermitirNegativos = false;
            this.txtCodigoEAN.Size = new System.Drawing.Size(188, 20);
            this.txtCodigoEAN.TabIndex = 0;
            this.txtCodigoEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoEAN.TextChanged += new System.EventHandler(this.txtCodigoEAN_TextChanged);
            this.txtCodigoEAN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoEAN_KeyDown);
            this.txtCodigoEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoEAN_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(25, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 19);
            this.label4.TabIndex = 15;
            this.label4.Text = "Código EAN :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmET_Producto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 138);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmET_Producto";
            this.Text = "Generar Etiqueta Producto";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmET_Producto_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatos.ResumeLayout(false);
            this.FrameDatos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.GroupBox FrameDatos;
        private SC_ControlsCS.scTextBoxExt txtCodigoEAN;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scLabelExt lblProducto;
        private SC_ControlsCS.scLabelExt lblLaboratorio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
    }
}