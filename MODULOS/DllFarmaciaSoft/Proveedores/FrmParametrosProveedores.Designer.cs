namespace DllFarmaciaSoft.Proveedores
{
    partial class FrmParametrosProveedores
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.grdParametros = new FarPoint.Win.Spread.FpSpread();
            this.grdParametros_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.grdParametros);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(833, 402);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametros";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(13, 342);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(809, 50);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "label1";
            // 
            // grdParametros
            // 
            this.grdParametros.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.grdParametros.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdParametros.Location = new System.Drawing.Point(13, 17);
            this.grdParametros.Name = "grdParametros";
            this.grdParametros.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdParametros.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdParametros_Sheet1});
            this.grdParametros.Size = new System.Drawing.Size(809, 322);
            this.grdParametros.TabIndex = 0;
            this.grdParametros.LeaveCell += new FarPoint.Win.Spread.LeaveCellEventHandler(this.grdParametros_LeaveCell);
            // 
            // grdParametros_Sheet1
            // 
            this.grdParametros_Sheet1.Reset();
            this.grdParametros_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdParametros_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdParametros_Sheet1.ColumnCount = 4;
            this.grdParametros_Sheet1.RowCount = 5;
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Arbol";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Parametro";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Valor";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Descripción";
            this.grdParametros_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdParametros_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(0).Label = "Arbol";
            this.grdParametros_Sheet1.Columns.Get(0).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(0).Visible = false;
            this.grdParametros_Sheet1.Columns.Get(0).Width = 87F;
            this.grdParametros_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdParametros_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdParametros_Sheet1.Columns.Get(1).Label = "Parametro";
            this.grdParametros_Sheet1.Columns.Get(1).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(1).Width = 266F;
            textCellType3.MaxLength = 300;
            this.grdParametros_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdParametros_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdParametros_Sheet1.Columns.Get(2).Label = "Valor";
            this.grdParametros_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(2).Width = 487F;
            this.grdParametros_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdParametros_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdParametros_Sheet1.Columns.Get(3).Label = "Descripción";
            this.grdParametros_Sheet1.Columns.Get(3).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(3).Visible = false;
            this.grdParametros_Sheet1.Columns.Get(3).Width = 245F;
            this.grdParametros_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdParametros_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(598, 411);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(118, 28);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(722, 411);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(118, 28);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmParametrosProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 446);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmParametrosProveedores";
            this.Text = "Configuración de Parametros de Proveedores";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmParametrosProveedores_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdParametros;
        private FarPoint.Win.Spread.SheetView grdParametros_Sheet1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblDescripcion;
    }
}