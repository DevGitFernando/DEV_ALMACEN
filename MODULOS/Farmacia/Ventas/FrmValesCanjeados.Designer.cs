namespace Farmacia.Ventas
{
    partial class FrmValesCanjeados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmValesCanjeados));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPersonal = new System.Windows.Forms.Label();
            this.txtIdPersonal = new SC_ControlsCS.scTextBoxExt();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtVenta = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaCanje = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(884, 25);
            this.toolStripBarraMenu.TabIndex = 4;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
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
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblPersonal);
            this.groupBox3.Controls.Add(this.txtIdPersonal);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(9, 503);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(866, 51);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Información de registro de venta";
            // 
            // lblPersonal
            // 
            this.lblPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonal.Location = new System.Drawing.Point(152, 18);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Size = new System.Drawing.Size(694, 21);
            this.lblPersonal.TabIndex = 9;
            this.lblPersonal.Text = "Proveedor :";
            this.lblPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdPersonal
            // 
            this.txtIdPersonal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPersonal.Decimales = 2;
            this.txtIdPersonal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPersonal.ForeColor = System.Drawing.Color.Black;
            this.txtIdPersonal.Location = new System.Drawing.Point(70, 18);
            this.txtIdPersonal.Name = "txtIdPersonal";
            this.txtIdPersonal.PermitirApostrofo = false;
            this.txtIdPersonal.PermitirNegativos = false;
            this.txtIdPersonal.Size = new System.Drawing.Size(76, 20);
            this.txtIdPersonal.TabIndex = 0;
            this.txtIdPersonal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(14, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "Personal :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdClaves);
            this.groupBox2.Location = new System.Drawing.Point(9, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(866, 393);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Venta";
            // 
            // grdClaves
            // 
            this.grdClaves.AccessibleDescription = "grdClaves, Sheet1, Row 0, Column 0, ";
            this.grdClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdClaves.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdClaves.Location = new System.Drawing.Point(9, 19);
            this.grdClaves.Name = "grdClaves";
            this.grdClaves.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClaves.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdClaves.Size = new System.Drawing.Size(849, 367);
            this.grdClaves.TabIndex = 0;
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 3;
            this.grdProductos_Sheet1.RowCount = 13;
            this.grdProductos_Sheet1.Cells.Get(1, 1).Locked = false;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 97F;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 438F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Cantidad";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 70F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtVenta);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaCanje);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtFolio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(866, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos generales de Vale";
            // 
            // txtVenta
            // 
            this.txtVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVenta.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtVenta.Decimales = 2;
            this.txtVenta.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtVenta.ForeColor = System.Drawing.Color.Black;
            this.txtVenta.Location = new System.Drawing.Point(768, 16);
            this.txtVenta.MaxLength = 8;
            this.txtVenta.Name = "txtVenta";
            this.txtVenta.PermitirApostrofo = false;
            this.txtVenta.PermitirNegativos = false;
            this.txtVenta.Size = new System.Drawing.Size(92, 20);
            this.txtVenta.TabIndex = 52;
            this.txtVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(664, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 12);
            this.label2.TabIndex = 53;
            this.label2.Text = "Folio de Venta :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaCanje
            // 
            this.dtpFechaCanje.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaCanje.Enabled = false;
            this.dtpFechaCanje.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCanje.Location = new System.Drawing.Point(120, 42);
            this.dtpFechaCanje.Name = "dtpFechaCanje";
            this.dtpFechaCanje.Size = new System.Drawing.Size(92, 20);
            this.dtpFechaCanje.TabIndex = 50;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(19, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Fecha de Canje :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(767, 42);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(92, 20);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(661, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(218, 15);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(98, 20);
            this.lblCancelado.TabIndex = 32;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(120, 16);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(92, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(78, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmValesCanjeados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmValesCanjeados";
            this.Text = "Captura de Vales Canjeados";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmVentas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmValesCanjeados_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblPersonal;
        private SC_ControlsCS.scTextBoxExt txtIdPersonal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private FarPoint.Win.Spread.FpSpread grdClaves;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.DateTimePicker dtpFechaCanje;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtVenta;
        private System.Windows.Forms.Label label2;
    }
}