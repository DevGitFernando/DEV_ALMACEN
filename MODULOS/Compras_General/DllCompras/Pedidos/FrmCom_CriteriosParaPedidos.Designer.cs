namespace DllCompras.Pedidos
{
    partial class FrmCom_CriteriosParaPedidos
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCom_CriteriosParaPedidos));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCantidadRequerida = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDescripcionSSA = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.lblCodigoEAN = new System.Windows.Forms.Label();
            this.lblArticulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCantidadSolicitadaProv = new System.Windows.Forms.Label();
            this.grdProveedoresClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdProveedoresClaves_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProveedoresClaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProveedoresClaves_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblCantidadRequerida);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblDescripcionSSA);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblClaveSSA);
            this.groupBox1.Controls.Add(this.lblCodigoEAN);
            this.groupBox1.Controls.Add(this.lblArticulo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(799, 134);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Producto";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(449, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 15);
            this.label7.TabIndex = 21;
            this.label7.Text = "Cajas / Unidades requeridas :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCantidadRequerida
            // 
            this.lblCantidadRequerida.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCantidadRequerida.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadRequerida.Location = new System.Drawing.Point(643, 18);
            this.lblCantidadRequerida.Name = "lblCantidadRequerida";
            this.lblCantidadRequerida.Size = new System.Drawing.Size(145, 20);
            this.lblCantidadRequerida.TabIndex = 20;
            this.lblCantidadRequerida.Text = "Producto :";
            this.lblCantidadRequerida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(223, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Nombre Comercial :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcionSSA
            // 
            this.lblDescripcionSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionSSA.Location = new System.Drawing.Point(95, 44);
            this.lblDescripcionSSA.Name = "lblDescripcionSSA";
            this.lblDescripcionSSA.Size = new System.Drawing.Size(693, 56);
            this.lblDescripcionSSA.TabIndex = 16;
            this.lblDescripcionSSA.Text = "label5";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(13, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "Clave SSA :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(95, 18);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(118, 20);
            this.lblClaveSSA.TabIndex = 15;
            this.lblClaveSSA.Text = "Producto :";
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCodigoEAN
            // 
            this.lblCodigoEAN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCodigoEAN.Location = new System.Drawing.Point(95, 105);
            this.lblCodigoEAN.Name = "lblCodigoEAN";
            this.lblCodigoEAN.Size = new System.Drawing.Size(118, 20);
            this.lblCodigoEAN.TabIndex = 0;
            this.lblCodigoEAN.Text = "Producto :";
            this.lblCodigoEAN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblArticulo
            // 
            this.lblArticulo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblArticulo.Location = new System.Drawing.Point(325, 105);
            this.lblArticulo.Name = "lblArticulo";
            this.lblArticulo.Size = new System.Drawing.Size(463, 20);
            this.lblArticulo.TabIndex = 2;
            this.lblArticulo.Text = "label5";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Código EAN :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblCantidadSolicitadaProv);
            this.groupBox2.Controls.Add(this.grdProveedoresClaves);
            this.groupBox2.Location = new System.Drawing.Point(12, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(799, 341);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proveedores surten Clave-Codigo EAN";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(373, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 15);
            this.label2.TabIndex = 23;
            this.label2.Text = "Total de Cajas / Unidades Requeridas :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCantidadSolicitadaProv
            // 
            this.lblCantidadSolicitadaProv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCantidadSolicitadaProv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadSolicitadaProv.Location = new System.Drawing.Point(624, 306);
            this.lblCantidadSolicitadaProv.Name = "lblCantidadSolicitadaProv";
            this.lblCantidadSolicitadaProv.Size = new System.Drawing.Size(145, 20);
            this.lblCantidadSolicitadaProv.TabIndex = 22;
            this.lblCantidadSolicitadaProv.Text = "Producto :";
            this.lblCantidadSolicitadaProv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdProveedoresClaves
            // 
            this.grdProveedoresClaves.AccessibleDescription = "grdProveedoresClaves, Sheet1, Row 0, Column 0, ";
            this.grdProveedoresClaves.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProveedoresClaves.Location = new System.Drawing.Point(9, 19);
            this.grdProveedoresClaves.Name = "grdProveedoresClaves";
            this.grdProveedoresClaves.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProveedoresClaves.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProveedoresClaves_Sheet1});
            this.grdProveedoresClaves.Size = new System.Drawing.Size(779, 285);
            this.grdProveedoresClaves.TabIndex = 0;
            this.grdProveedoresClaves.EditModeOff += new System.EventHandler(this.grdProveedoresClaves_EditModeOff);
            // 
            // grdProveedoresClaves_Sheet1
            // 
            this.grdProveedoresClaves_Sheet1.Reset();
            this.grdProveedoresClaves_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProveedoresClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProveedoresClaves_Sheet1.ColumnCount = 6;
            this.grdProveedoresClaves_Sheet1.RowCount = 5;
            this.grdProveedoresClaves_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave Proveedor";
            this.grdProveedoresClaves_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Proveedor";
            this.grdProveedoresClaves_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Precio";
            this.grdProveedoresClaves_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "% Surtimiento";
            this.grdProveedoresClaves_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Tiempo de Entrega";
            this.grdProveedoresClaves_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cantidad a Solicitar";
            this.grdProveedoresClaves_Sheet1.ColumnHeader.Rows.Get(0).Height = 41F;
            this.grdProveedoresClaves_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProveedoresClaves_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProveedoresClaves_Sheet1.Columns.Get(0).Label = "Clave Proveedor";
            this.grdProveedoresClaves_Sheet1.Columns.Get(0).Locked = true;
            this.grdProveedoresClaves_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProveedoresClaves_Sheet1.Columns.Get(0).Width = 70F;
            this.grdProveedoresClaves_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdProveedoresClaves_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProveedoresClaves_Sheet1.Columns.Get(1).Label = "Proveedor";
            this.grdProveedoresClaves_Sheet1.Columns.Get(1).Locked = true;
            this.grdProveedoresClaves_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProveedoresClaves_Sheet1.Columns.Get(1).Width = 330F;
            this.grdProveedoresClaves_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProveedoresClaves_Sheet1.Columns.Get(2).Label = "Precio";
            this.grdProveedoresClaves_Sheet1.Columns.Get(2).Locked = true;
            this.grdProveedoresClaves_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProveedoresClaves_Sheet1.Columns.Get(2).Width = 80F;
            this.grdProveedoresClaves_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProveedoresClaves_Sheet1.Columns.Get(3).Label = "% Surtimiento";
            this.grdProveedoresClaves_Sheet1.Columns.Get(3).Locked = true;
            this.grdProveedoresClaves_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProveedoresClaves_Sheet1.Columns.Get(3).Width = 80F;
            this.grdProveedoresClaves_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProveedoresClaves_Sheet1.Columns.Get(4).Label = "Tiempo de Entrega";
            this.grdProveedoresClaves_Sheet1.Columns.Get(4).Locked = true;
            this.grdProveedoresClaves_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProveedoresClaves_Sheet1.Columns.Get(4).Width = 80F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            this.grdProveedoresClaves_Sheet1.Columns.Get(5).CellType = numberCellType1;
            this.grdProveedoresClaves_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProveedoresClaves_Sheet1.Columns.Get(5).Label = "Cantidad a Solicitar";
            this.grdProveedoresClaves_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProveedoresClaves_Sheet1.Columns.Get(5).Width = 80F;
            this.grdProveedoresClaves_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdProveedoresClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(819, 25);
            this.toolStripBarraMenu.TabIndex = 14;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmCom_CriteriosParaPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 513);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCom_CriteriosParaPedidos";
            this.Text = "Asignación de Pedidos para Proveedores";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCom_CriteriosParaPedidos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProveedoresClaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProveedoresClaves_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDescripcionSSA;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblClaveSSA;
        private System.Windows.Forms.Label lblArticulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCodigoEAN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCantidadRequerida;
        private FarPoint.Win.Spread.FpSpread grdProveedoresClaves;
        private FarPoint.Win.Spread.SheetView grdProveedoresClaves_Sheet1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCantidadSolicitadaProv;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}