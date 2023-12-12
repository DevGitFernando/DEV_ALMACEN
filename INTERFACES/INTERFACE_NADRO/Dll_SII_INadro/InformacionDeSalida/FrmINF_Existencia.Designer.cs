﻿namespace Dll_SII_INadro.InformacionDeSalida
{
    partial class FrmINF_Existencia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmINF_Existencia));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstvUnidades = new System.Windows.Forms.ListView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnGenerarExistenciaGeneral = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGenerarExistenciaPorFarmacia = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrarDocumento = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lstvUnidades);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(970, 385);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unidades";
            // 
            // lstvUnidades
            // 
            this.lstvUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvUnidades.ContextMenuStrip = this.contextMenu;
            this.lstvUnidades.Location = new System.Drawing.Point(10, 19);
            this.lstvUnidades.Name = "lstvUnidades";
            this.lstvUnidades.Size = new System.Drawing.Size(952, 357);
            this.lstvUnidades.TabIndex = 0;
            this.lstvUnidades.UseCompatibleStateImageBehavior = false;
            this.lstvUnidades.SelectedIndexChanged += new System.EventHandler(this.lstvUnidades_SelectedIndexChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGenerarExistenciaGeneral,
            this.btnGenerarExistenciaPorFarmacia});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(240, 70);
            // 
            // btnGenerarExistenciaGeneral
            // 
            this.btnGenerarExistenciaGeneral.Name = "btnGenerarExistenciaGeneral";
            this.btnGenerarExistenciaGeneral.Size = new System.Drawing.Size(239, 22);
            this.btnGenerarExistenciaGeneral.Text = "Generar existencia general";
            this.btnGenerarExistenciaGeneral.Click += new System.EventHandler(this.btnGenerarExistenciaGeneral_Click);
            // 
            // btnGenerarExistenciaPorFarmacia
            // 
            this.btnGenerarExistenciaPorFarmacia.Name = "btnGenerarExistenciaPorFarmacia";
            this.btnGenerarExistenciaPorFarmacia.Size = new System.Drawing.Size(239, 22);
            this.btnGenerarExistenciaPorFarmacia.Text = "Generar existencia por farmacia";
            this.btnGenerarExistenciaPorFarmacia.Click += new System.EventHandler(this.btnGenerarExistenciaPorFarmacia_Click);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator2,
            this.btnEjecutar,
            this.toolStripSeparator3,
            this.btnIntegrarDocumento,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(994, 25);
            this.toolStripBarraMenu.TabIndex = 3;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnIntegrarDocumento
            // 
            this.btnIntegrarDocumento.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegrarDocumento.Image = ((System.Drawing.Image)(resources.GetObject("btnIntegrarDocumento.Image")));
            this.btnIntegrarDocumento.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegrarDocumento.Name = "btnIntegrarDocumento";
            this.btnIntegrarDocumento.Size = new System.Drawing.Size(23, 22);
            this.btnIntegrarDocumento.Text = "Generar pedido masivo";
            this.btnIntegrarDocumento.Click += new System.EventHandler(this.btnIntegrarDocumento_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmINF_Existencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 421);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmINF_Existencia";
            this.Text = "Generar documentos de existencia";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmINF_Existencia_Load);
            this.groupBox1.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstvUnidades;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnIntegrarDocumento;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem btnGenerarExistenciaGeneral;
        private System.Windows.Forms.ToolStripMenuItem btnGenerarExistenciaPorFarmacia;
    }
}