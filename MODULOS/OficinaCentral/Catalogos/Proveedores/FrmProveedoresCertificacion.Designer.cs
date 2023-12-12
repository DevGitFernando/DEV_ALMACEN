namespace OficinaCentral.Catalogos.Proveedores
{
    partial class FrmProveedoresCertificacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProveedoresCertificacion));
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCertificar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FrameRptes = new System.Windows.Forms.GroupBox();
            this.rdoRpteSanitario = new System.Windows.Forms.RadioButton();
            this.rdoRpteLegal = new System.Windows.Forms.RadioButton();
            this.btnAgregarDocto = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDoctos = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDirectorio = new System.Windows.Forms.Label();
            this.lblProv = new SC_ControlsCS.scLabelExt();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtProv = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdDocumentos = new FarPoint.Win.Spread.FpSpread();
            this.grdDocumentos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameRptes.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocumentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocumentos_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCertificar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(784, 25);
            this.toolStripBarraMenu.TabIndex = 5;
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
            // btnCertificar
            // 
            this.btnCertificar.AutoSize = false;
            this.btnCertificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCertificar.Image = ((System.Drawing.Image)(resources.GetObject("btnCertificar.Image")));
            this.btnCertificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCertificar.Name = "btnCertificar";
            this.btnCertificar.Size = new System.Drawing.Size(23, 22);
            this.btnCertificar.Text = "Certificar Proveedor";
            this.btnCertificar.Click += new System.EventHandler(this.btnCertificar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.FrameRptes);
            this.groupBox1.Controls.Add(this.btnAgregarDocto);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboDoctos);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblDirectorio);
            this.groupBox1.Controls.Add(this.lblProv);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtProv);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 162);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Proveedor:";
            // 
            // FrameRptes
            // 
            this.FrameRptes.Controls.Add(this.rdoRpteSanitario);
            this.FrameRptes.Controls.Add(this.rdoRpteLegal);
            this.FrameRptes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrameRptes.Location = new System.Drawing.Point(82, 101);
            this.FrameRptes.Name = "FrameRptes";
            this.FrameRptes.Size = new System.Drawing.Size(268, 51);
            this.FrameRptes.TabIndex = 33;
            this.FrameRptes.TabStop = false;
            this.FrameRptes.Text = "Representantes";
            // 
            // rdoRpteSanitario
            // 
            this.rdoRpteSanitario.Location = new System.Drawing.Point(153, 19);
            this.rdoRpteSanitario.Name = "rdoRpteSanitario";
            this.rdoRpteSanitario.Size = new System.Drawing.Size(89, 23);
            this.rdoRpteSanitario.TabIndex = 1;
            this.rdoRpteSanitario.TabStop = true;
            this.rdoRpteSanitario.Text = "Sanitario";
            this.rdoRpteSanitario.UseVisualStyleBackColor = true;
            // 
            // rdoRpteLegal
            // 
            this.rdoRpteLegal.Checked = true;
            this.rdoRpteLegal.Location = new System.Drawing.Point(40, 19);
            this.rdoRpteLegal.Name = "rdoRpteLegal";
            this.rdoRpteLegal.Size = new System.Drawing.Size(87, 23);
            this.rdoRpteLegal.TabIndex = 0;
            this.rdoRpteLegal.TabStop = true;
            this.rdoRpteLegal.Text = "Legal";
            this.rdoRpteLegal.UseVisualStyleBackColor = true;
            // 
            // btnAgregarDocto
            // 
            this.btnAgregarDocto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarDocto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarDocto.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarDocto.Image")));
            this.btnAgregarDocto.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregarDocto.Location = new System.Drawing.Point(689, 105);
            this.btnAgregarDocto.Name = "btnAgregarDocto";
            this.btnAgregarDocto.Size = new System.Drawing.Size(51, 47);
            this.btnAgregarDocto.TabIndex = 32;
            this.btnAgregarDocto.Text = " ";
            this.btnAgregarDocto.UseVisualStyleBackColor = true;
            this.btnAgregarDocto.Click += new System.EventHandler(this.btnAgregarDocto_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 31;
            this.label2.Text = "Documento :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDoctos
            // 
            this.cboDoctos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDoctos.BackColorEnabled = System.Drawing.Color.White;
            this.cboDoctos.Data = "";
            this.cboDoctos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoctos.Filtro = " 1 = 1";
            this.cboDoctos.ListaItemsBusqueda = 20;
            this.cboDoctos.Location = new System.Drawing.Point(82, 49);
            this.cboDoctos.MostrarToolTip = false;
            this.cboDoctos.Name = "cboDoctos";
            this.cboDoctos.Size = new System.Drawing.Size(658, 21);
            this.cboDoctos.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(1, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 29;
            this.label3.Text = "Directorio :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDirectorio
            // 
            this.lblDirectorio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDirectorio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDirectorio.Location = new System.Drawing.Point(82, 78);
            this.lblDirectorio.Name = "lblDirectorio";
            this.lblDirectorio.Size = new System.Drawing.Size(658, 20);
            this.lblDirectorio.TabIndex = 28;
            this.lblDirectorio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProv
            // 
            this.lblProv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProv.Location = new System.Drawing.Point(206, 23);
            this.lblProv.MostrarToolTip = false;
            this.lblProv.Name = "lblProv";
            this.lblProv.Size = new System.Drawing.Size(437, 20);
            this.lblProv.TabIndex = 5;
            this.lblProv.Text = "scLabelExt1";
            this.lblProv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCancelado
            // 
            this.lblCancelado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(659, 26);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(81, 13);
            this.lblCancelado.TabIndex = 4;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.Visible = false;
            // 
            // txtProv
            // 
            this.txtProv.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtProv.Decimales = 2;
            this.txtProv.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtProv.ForeColor = System.Drawing.Color.Black;
            this.txtProv.Location = new System.Drawing.Point(82, 23);
            this.txtProv.MaxLength = 4;
            this.txtProv.Name = "txtProv";
            this.txtProv.PermitirApostrofo = false;
            this.txtProv.PermitirNegativos = false;
            this.txtProv.Size = new System.Drawing.Size(118, 20);
            this.txtProv.TabIndex = 0;
            this.txtProv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtProv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProv_KeyDown);
            this.txtProv.Validating += new System.ComponentModel.CancelEventHandler(this.txtProv_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Proveedor :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdDocumentos);
            this.groupBox2.Location = new System.Drawing.Point(12, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 309);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Documentos";
            // 
            // grdDocumentos
            // 
            this.grdDocumentos.AccessibleDescription = "grdClaves, Sheet1, Row 0, Column 0, ";
            this.grdDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDocumentos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdDocumentos.Location = new System.Drawing.Point(9, 19);
            this.grdDocumentos.Name = "grdDocumentos";
            this.grdDocumentos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdDocumentos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdDocumentos_Sheet1});
            this.grdDocumentos.Size = new System.Drawing.Size(741, 282);
            this.grdDocumentos.TabIndex = 0;
            // 
            // grdDocumentos_Sheet1
            // 
            this.grdDocumentos_Sheet1.Reset();
            this.grdDocumentos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdDocumentos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdDocumentos_Sheet1.ColumnCount = 2;
            this.grdDocumentos_Sheet1.RowCount = 10;
            this.grdDocumentos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdDocto";
            this.grdDocumentos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Documento";
            this.grdDocumentos_Sheet1.ColumnHeader.Rows.Get(0).Height = 37F;
            this.grdDocumentos_Sheet1.Columns.Get(0).CellType = textCellType3;
            this.grdDocumentos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDocumentos_Sheet1.Columns.Get(0).Label = "IdDocto";
            this.grdDocumentos_Sheet1.Columns.Get(0).Locked = true;
            this.grdDocumentos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDocumentos_Sheet1.Columns.Get(0).Visible = false;
            this.grdDocumentos_Sheet1.Columns.Get(0).Width = 70F;
            this.grdDocumentos_Sheet1.Columns.Get(1).CellType = textCellType4;
            this.grdDocumentos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdDocumentos_Sheet1.Columns.Get(1).Label = "Documento";
            this.grdDocumentos_Sheet1.Columns.Get(1).Locked = true;
            this.grdDocumentos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDocumentos_Sheet1.Columns.Get(1).Width = 580F;
            this.grdDocumentos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdDocumentos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmProveedoresCertificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmProveedoresCertificacion";
            this.Text = "Certificación de Proveedores";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmProveedoresCertificacion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameRptes.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDocumentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocumentos_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCertificar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtProv;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scLabelExt lblProv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDirectorio;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboDoctos;
        private System.Windows.Forms.Button btnAgregarDocto;
        private System.Windows.Forms.GroupBox FrameRptes;
        private System.Windows.Forms.RadioButton rdoRpteSanitario;
        private System.Windows.Forms.RadioButton rdoRpteLegal;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdDocumentos;
        private FarPoint.Win.Spread.SheetView grdDocumentos_Sheet1;
    }
}