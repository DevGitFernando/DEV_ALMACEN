﻿namespace Dll_SII_IRFID.Monitor
{
    partial class FrmValidarEntrada
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmValidarEntrada));
            this.FrameInformacionGeneral = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.lblTitulo__Estado = new System.Windows.Forms.Label();
            this.txtFolioMovto = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrarPaquetesDeDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIniciarEscaneo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDetenerEscaneo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listvwResultados = new System.Windows.Forms.ListView();
            this.colNumTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTAG = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colResultado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FrameInformacionGeneral.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameInformacionGeneral
            // 
            this.FrameInformacionGeneral.Controls.Add(this.cboFarmacias);
            this.FrameInformacionGeneral.Controls.Add(this.lblTitulo__Estado);
            this.FrameInformacionGeneral.Controls.Add(this.txtFolioMovto);
            this.FrameInformacionGeneral.Controls.Add(this.label2);
            this.FrameInformacionGeneral.Controls.Add(this.dtpFechaRegistro);
            this.FrameInformacionGeneral.Controls.Add(this.label3);
            this.FrameInformacionGeneral.Location = new System.Drawing.Point(10, 28);
            this.FrameInformacionGeneral.Name = "FrameInformacionGeneral";
            this.FrameInformacionGeneral.Size = new System.Drawing.Size(785, 72);
            this.FrameInformacionGeneral.TabIndex = 1;
            this.FrameInformacionGeneral.TabStop = false;
            this.FrameInformacionGeneral.Text = "Información general";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(110, 17);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(660, 21);
            this.cboFarmacias.TabIndex = 0;
            // 
            // lblTitulo__Estado
            // 
            this.lblTitulo__Estado.Location = new System.Drawing.Point(11, 19);
            this.lblTitulo__Estado.Name = "lblTitulo__Estado";
            this.lblTitulo__Estado.Size = new System.Drawing.Size(95, 17);
            this.lblTitulo__Estado.TabIndex = 48;
            this.lblTitulo__Estado.Text = "Unidad origen :";
            this.lblTitulo__Estado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioMovto
            // 
            this.txtFolioMovto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFolioMovto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioMovto.Decimales = 2;
            this.txtFolioMovto.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolioMovto.ForeColor = System.Drawing.Color.Black;
            this.txtFolioMovto.Location = new System.Drawing.Point(110, 42);
            this.txtFolioMovto.MaxLength = 10;
            this.txtFolioMovto.Name = "txtFolioMovto";
            this.txtFolioMovto.PermitirApostrofo = false;
            this.txtFolioMovto.PermitirNegativos = false;
            this.txtFolioMovto.Size = new System.Drawing.Size(186, 20);
            this.txtFolioMovto.TabIndex = 1;
            this.txtFolioMovto.Text = "0123456789";
            this.txtFolioMovto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolioMovto.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolioMovto_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Folio :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(679, 42);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(576, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fecha de Registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator4,
            this.btnIntegrarPaquetesDeDatos,
            this.toolStripSeparator5,
            this.btnIniciarEscaneo,
            this.toolStripSeparator2,
            this.btnDetenerEscaneo,
            this.toolStripSeparator3});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(805, 25);
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
            this.btnNuevo.Text = "Nuevo";
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
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnIntegrarPaquetesDeDatos
            // 
            this.btnIntegrarPaquetesDeDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegrarPaquetesDeDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnIntegrarPaquetesDeDatos.Image")));
            this.btnIntegrarPaquetesDeDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegrarPaquetesDeDatos.Name = "btnIntegrarPaquetesDeDatos";
            this.btnIntegrarPaquetesDeDatos.Size = new System.Drawing.Size(23, 22);
            this.btnIntegrarPaquetesDeDatos.Text = "Integrar transferencias";
            this.btnIntegrarPaquetesDeDatos.Click += new System.EventHandler(this.btnIntegrarPaquetesDeDatos_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnIniciarEscaneo
            // 
            this.btnIniciarEscaneo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIniciarEscaneo.Image = ((System.Drawing.Image)(resources.GetObject("btnIniciarEscaneo.Image")));
            this.btnIniciarEscaneo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIniciarEscaneo.Name = "btnIniciarEscaneo";
            this.btnIniciarEscaneo.Size = new System.Drawing.Size(23, 22);
            this.btnIniciarEscaneo.Text = "Iniciar proceso de escaneo";
            this.btnIniciarEscaneo.ToolTipText = "Iniciar proceso de escaneo";
            this.btnIniciarEscaneo.Click += new System.EventHandler(this.btnIniciarEscaneo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDetenerEscaneo
            // 
            this.btnDetenerEscaneo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDetenerEscaneo.Image = ((System.Drawing.Image)(resources.GetObject("btnDetenerEscaneo.Image")));
            this.btnDetenerEscaneo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDetenerEscaneo.Name = "btnDetenerEscaneo";
            this.btnDetenerEscaneo.Size = new System.Drawing.Size(23, 22);
            this.btnDetenerEscaneo.Text = "Detener proceso de escaneo";
            this.btnDetenerEscaneo.Click += new System.EventHandler(this.btnDetenerEscaneo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listvwResultados);
            this.groupBox2.Location = new System.Drawing.Point(8, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(785, 449);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles";
            // 
            // listvwResultados
            // 
            this.listvwResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listvwResultados.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNumTag,
            this.colTAG,
            this.colResultado});
            this.listvwResultados.FullRowSelect = true;
            this.listvwResultados.Location = new System.Drawing.Point(10, 20);
            this.listvwResultados.MultiSelect = false;
            this.listvwResultados.Name = "listvwResultados";
            this.listvwResultados.Size = new System.Drawing.Size(766, 420);
            this.listvwResultados.TabIndex = 0;
            this.listvwResultados.UseCompatibleStateImageBehavior = false;
            this.listvwResultados.View = System.Windows.Forms.View.Details;
            // 
            // colNumTag
            // 
            this.colNumTag.Text = "Registro";
            this.colNumTag.Width = 120;
            // 
            // colTAG
            // 
            this.colTAG.Text = "Tag escaneado";
            this.colTAG.Width = 277;
            // 
            // colResultado
            // 
            this.colResultado.Text = "Resultado";
            this.colResultado.Width = 340;
            // 
            // FrmValidarEntrada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 562);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameInformacionGeneral);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmValidarEntrada";
            this.Text = "Validación de entradas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmValidarEntrada_FormClosing);
            this.Load += new System.EventHandler(this.FrmValidarEntrada_Load);
            this.FrameInformacionGeneral.ResumeLayout(false);
            this.FrameInformacionGeneral.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameInformacionGeneral;
        private SC_ControlsCS.scTextBoxExt txtFolioMovto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripButton btnIniciarEscaneo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDetenerEscaneo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listvwResultados;
        private System.Windows.Forms.ColumnHeader colTAG;
        private System.Windows.Forms.ColumnHeader colResultado;
        private System.Windows.Forms.ColumnHeader colNumTag;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnIntegrarPaquetesDeDatos;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label lblTitulo__Estado;
    }
}