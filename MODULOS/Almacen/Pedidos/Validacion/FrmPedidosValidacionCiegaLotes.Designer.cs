namespace Almacen.Pedidos.Validacion
{
    partial class FrmPedidosValidacionCiegaLotes
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
            FarPoint.Win.Spread.NamedStyle namedStyle1 = new FarPoint.Win.Spread.NamedStyle("DataAreaMidnght");
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType1 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.NamedStyle namedStyle2 = new FarPoint.Win.Spread.NamedStyle("CornerEnhanced");
            FarPoint.Win.Spread.CellType.EnhancedCornerRenderer enhancedCornerRenderer1 = new FarPoint.Win.Spread.CellType.EnhancedCornerRenderer();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.FrameLotes = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grdLotes = new FarPoint.Win.Spread.FpSpread();
            this.grdLotes_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDescripcionSSA = new SC_ControlsCS.scLabelExt();
            this.label11 = new System.Windows.Forms.Label();
            this.lblCodigoEAN = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblContenido = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.lblPresentacion = new System.Windows.Forms.Label();
            this.lblArticulo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.FrameLotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameLotes
            // 
            this.FrameLotes.Controls.Add(this.lblTotal);
            this.FrameLotes.Controls.Add(this.label3);
            this.FrameLotes.Controls.Add(this.grdLotes);
            this.FrameLotes.Location = new System.Drawing.Point(807, 4);
            this.FrameLotes.Margin = new System.Windows.Forms.Padding(4);
            this.FrameLotes.Name = "FrameLotes";
            this.FrameLotes.Padding = new System.Windows.Forms.Padding(4);
            this.FrameLotes.Size = new System.Drawing.Size(407, 464);
            this.FrameLotes.TabIndex = 3;
            this.FrameLotes.TabStop = false;
            this.FrameLotes.Text = "Lotes del producto";
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Location = new System.Drawing.Point(237, 423);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(161, 25);
            this.lblTotal.TabIndex = 12;
            this.lblTotal.Text = "Total cantidad :";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(115, 423);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "Total cantidad :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdLotes
            // 
            this.grdLotes.AccessibleDescription = "grdLotes, Sheet1, Row 0, Column 0, ";
            this.grdLotes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdLotes.Location = new System.Drawing.Point(16, 21);
            this.grdLotes.Margin = new System.Windows.Forms.Padding(4);
            this.grdLotes.Name = "grdLotes";
            namedStyle1.BackColor = System.Drawing.Color.DarkGray;
            namedStyle1.CellType = generalCellType1;
            namedStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            namedStyle1.Renderer = generalCellType1;
            namedStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(196)))), ((int)(((byte)(233)))));
            namedStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle2.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle2.Renderer = enhancedCornerRenderer1;
            namedStyle2.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes.NamedStyles.AddRange(new FarPoint.Win.Spread.NamedStyle[] {
            namedStyle1,
            namedStyle2});
            this.grdLotes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdLotes.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdLotes_Sheet1});
            this.grdLotes.Size = new System.Drawing.Size(382, 389);
            this.grdLotes.TabIndex = 0;
            this.grdLotes.EditModeOff += new System.EventHandler(this.grdLotes_EditModeOff);
            this.grdLotes.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdLotes_Advance);
            // 
            // grdLotes_Sheet1
            // 
            this.grdLotes_Sheet1.Reset();
            this.grdLotes_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdLotes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdLotes_Sheet1.ColumnCount = 4;
            this.grdLotes_Sheet1.RowCount = 5;
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Codigo EAN";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Clave de lote";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Cantidad";
            this.grdLotes_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdLotes_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdLotes_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(0).Label = "Código";
            this.grdLotes_Sheet1.Columns.Get(0).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(0).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(0).Width = 80F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            this.grdLotes_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdLotes_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(1).Label = "Codigo EAN";
            this.grdLotes_Sheet1.Columns.Get(1).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(1).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(1).Width = 122F;
            this.grdLotes_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdLotes_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdLotes_Sheet1.Columns.Get(2).Label = "Clave de lote";
            this.grdLotes_Sheet1.Columns.Get(2).Locked = false;
            this.grdLotes_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(2).Width = 150F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdLotes_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdLotes_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(3).Label = "Cantidad";
            this.grdLotes_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(3).Width = 70F;
            this.grdLotes_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdLotes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDescripcionSSA);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblCodigoEAN);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblContenido);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblClaveSSA);
            this.groupBox1.Controls.Add(this.lblPresentacion);
            this.groupBox1.Controls.Add(this.lblArticulo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblCodigo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(13, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(786, 464);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Producto";
            // 
            // lblDescripcionSSA
            // 
            this.lblDescripcionSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionSSA.Location = new System.Drawing.Point(143, 318);
            this.lblDescripcionSSA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcionSSA.MostrarToolTip = false;
            this.lblDescripcionSSA.Name = "lblDescripcionSSA";
            this.lblDescripcionSSA.Size = new System.Drawing.Size(628, 130);
            this.lblDescripcionSSA.TabIndex = 24;
            this.lblDescripcionSSA.Text = "scLabelExt1";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(291, 28);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 18);
            this.label11.TabIndex = 23;
            this.label11.Text = "Código EAN :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCodigoEAN
            // 
            this.lblCodigoEAN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCodigoEAN.Location = new System.Drawing.Point(399, 25);
            this.lblCodigoEAN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigoEAN.Name = "lblCodigoEAN";
            this.lblCodigoEAN.Size = new System.Drawing.Size(177, 25);
            this.lblCodigoEAN.TabIndex = 22;
            this.lblCodigoEAN.Text = "Producto :";
            this.lblCodigoEAN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(393, 291);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "Contenido :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContenido
            // 
            this.lblContenido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContenido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContenido.Location = new System.Drawing.Point(503, 289);
            this.lblContenido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContenido.Name = "lblContenido";
            this.lblContenido.Size = new System.Drawing.Size(268, 25);
            this.lblContenido.TabIndex = 18;
            this.lblContenido.Text = "Producto :";
            this.lblContenido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(11, 292);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 18);
            this.label8.TabIndex = 17;
            this.label8.Text = "Clave SSA :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(143, 289);
            this.lblClaveSSA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(233, 25);
            this.lblClaveSSA.TabIndex = 15;
            this.lblClaveSSA.Text = "Producto :";
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPresentacion
            // 
            this.lblPresentacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPresentacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentacion.Location = new System.Drawing.Point(143, 253);
            this.lblPresentacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.Size = new System.Drawing.Size(628, 25);
            this.lblPresentacion.TabIndex = 14;
            this.lblPresentacion.Text = "label5";
            this.lblPresentacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblArticulo
            // 
            this.lblArticulo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblArticulo.Location = new System.Drawing.Point(143, 57);
            this.lblArticulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArticulo.Name = "lblArticulo";
            this.lblArticulo.Size = new System.Drawing.Size(628, 185);
            this.lblArticulo.TabIndex = 2;
            this.lblArticulo.Text = "label5";
            this.lblArticulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 256);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "Dispensar por :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Producto :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCodigo
            // 
            this.lblCodigo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCodigo.Location = new System.Drawing.Point(143, 25);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(141, 25);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Producto :";
            this.lblCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 318);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "Captura por :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Visible = false;
            // 
            // lblAyuda
            // 
            this.lblAyuda.BackColor = System.Drawing.Color.Black;
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyuda.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAyuda.Location = new System.Drawing.Point(0, 478);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(1225, 30);
            this.lblAyuda.TabIndex = 10;
            this.lblAyuda.Text = "<F12> Cerrar";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmPedidosValidacionCiegaLotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 508);
            this.ControlBox = false;
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameLotes);
            this.Name = "FrmPedidosValidacionCiegaLotes";
            this.Text = "Captura de lotes de producto";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPedidosValidacionCiegaLotes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPedidosValidacionCiegaLotes_KeyDown);
            this.FrameLotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox FrameLotes;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private FarPoint.Win.Spread.FpSpread grdLotes;
        private FarPoint.Win.Spread.SheetView grdLotes_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scLabelExt lblDescripcionSSA;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblCodigoEAN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblContenido;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblClaveSSA;
        private System.Windows.Forms.Label lblPresentacion;
        private System.Windows.Forms.Label lblArticulo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAyuda;
    }
}