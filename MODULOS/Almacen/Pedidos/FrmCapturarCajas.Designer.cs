namespace Almacen.Pedidos
{
    partial class FrmCapturarCajas
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
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer1 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer2 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCaja_02_Final = new System.Windows.Forms.NumericUpDown();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.txtCaja_01_Inicial = new System.Windows.Forms.NumericUpDown();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.FrameCajas = new System.Windows.Forms.GroupBox();
            this.grdCajas = new FarPoint.Win.Spread.FpSpread();
            this.grdCajas_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaja_02_Final)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaja_01_Inicial)).BeginInit();
            this.FrameCajas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCajas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCajas_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCaja_02_Final);
            this.groupBox1.Controls.Add(this.scLabelExt1);
            this.groupBox1.Controls.Add(this.txtCaja_01_Inicial);
            this.groupBox1.Controls.Add(this.scLabelExt2);
            this.groupBox1.Location = new System.Drawing.Point(16, 290);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(229, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            this.groupBox1.Visible = false;
            // 
            // txtCaja_02_Final
            // 
            this.txtCaja_02_Final.Location = new System.Drawing.Point(152, 69);
            this.txtCaja_02_Final.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCaja_02_Final.Name = "txtCaja_02_Final";
            this.txtCaja_02_Final.Size = new System.Drawing.Size(150, 30);
            this.txtCaja_02_Final.TabIndex = 9;
            this.txtCaja_02_Final.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(35, 69);
            this.scLabelExt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(110, 29);
            this.scLabelExt1.TabIndex = 8;
            this.scLabelExt1.Text = "Caja final : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCaja_01_Inicial
            // 
            this.txtCaja_01_Inicial.Location = new System.Drawing.Point(152, 29);
            this.txtCaja_01_Inicial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCaja_01_Inicial.Name = "txtCaja_01_Inicial";
            this.txtCaja_01_Inicial.Size = new System.Drawing.Size(150, 30);
            this.txtCaja_01_Inicial.TabIndex = 7;
            this.txtCaja_01_Inicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(35, 29);
            this.scLabelExt2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(110, 29);
            this.scLabelExt2.TabIndex = 1;
            this.scLabelExt2.Text = "Caja inical : ";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(409, 292);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(136, 35);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "[F12] Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(269, 292);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(136, 35);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "[F5] Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FrameCajas
            // 
            this.FrameCajas.Controls.Add(this.grdCajas);
            this.FrameCajas.Controls.Add(this.groupBox1);
            this.FrameCajas.Controls.Add(this.btnCancelar);
            this.FrameCajas.Controls.Add(this.btnAceptar);
            this.FrameCajas.Location = new System.Drawing.Point(13, 11);
            this.FrameCajas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FrameCajas.Name = "FrameCajas";
            this.FrameCajas.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FrameCajas.Size = new System.Drawing.Size(563, 344);
            this.FrameCajas.TabIndex = 10;
            this.FrameCajas.TabStop = false;
            this.FrameCajas.Text = "Información";
            // 
            // grdCajas
            // 
            this.grdCajas.AccessibleDescription = "grdCajas, Sheet1, Row 0, Column 0, ";
            this.grdCajas.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdCajas.HorizontalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdCajas.HorizontalScrollBar.Name = "";
            enhancedScrollBarRenderer1.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer1.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer1.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.grdCajas.HorizontalScrollBar.Renderer = enhancedScrollBarRenderer1;
            this.grdCajas.HorizontalScrollBar.TabIndex = 2;
            this.grdCajas.Location = new System.Drawing.Point(16, 27);
            this.grdCajas.Name = "grdCajas";
            this.grdCajas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdCajas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdCajas_Sheet1});
            this.grdCajas.Size = new System.Drawing.Size(529, 257);
            this.grdCajas.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell;
            this.grdCajas.TabIndex = 1;
            this.grdCajas.VerticalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdCajas.VerticalScrollBar.Name = "";
            enhancedScrollBarRenderer2.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer2.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer2.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.grdCajas.VerticalScrollBar.Renderer = enhancedScrollBarRenderer2;
            this.grdCajas.VerticalScrollBar.TabIndex = 3;
            this.grdCajas.EditModeOff += new System.EventHandler(this.grdCajas_EditModeOff);
            this.grdCajas.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdFolios_Advance);
            this.grdCajas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdCajas_KeyDown);
            // 
            // grdCajas_Sheet1
            // 
            this.grdCajas_Sheet1.Reset();
            this.grdCajas_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdCajas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdCajas_Sheet1.ColumnCount = 5;
            this.grdCajas_Sheet1.RowCount = 8;
            this.grdCajas_Sheet1.Cells.Get(1, 4).Locked = false;
            this.grdCajas_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id";
            this.grdCajas_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Renglon";
            this.grdCajas_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Fechamidicacion";
            this.grdCajas_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Caja inicial";
            this.grdCajas_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Caja final";
            this.grdCajas_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell";
            this.grdCajas_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            this.grdCajas_Sheet1.Columns.Get(0).CellType = numberCellType1;
            this.grdCajas_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCajas_Sheet1.Columns.Get(0).Label = "Id";
            this.grdCajas_Sheet1.Columns.Get(0).Locked = true;
            this.grdCajas_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 15;
            this.grdCajas_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.grdCajas_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCajas_Sheet1.Columns.Get(1).Label = "Renglon";
            this.grdCajas_Sheet1.Columns.Get(1).Locked = true;
            this.grdCajas_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCajas_Sheet1.Columns.Get(1).Width = 97F;
            this.grdCajas_Sheet1.Columns.Get(2).Label = "Fechamidicacion";
            this.grdCajas_Sheet1.Columns.Get(2).Locked = true;
            this.grdCajas_Sheet1.Columns.Get(2).Visible = false;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.MaximumValue = 99999D;
            numberCellType2.MinimumValue = 0D;
            this.grdCajas_Sheet1.Columns.Get(3).CellType = numberCellType2;
            this.grdCajas_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCajas_Sheet1.Columns.Get(3).Label = "Caja inicial";
            this.grdCajas_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCajas_Sheet1.Columns.Get(3).Width = 120F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.MaximumValue = 99999D;
            numberCellType3.MinimumValue = 0D;
            this.grdCajas_Sheet1.Columns.Get(4).CellType = numberCellType3;
            this.grdCajas_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCajas_Sheet1.Columns.Get(4).Label = "Caja final";
            this.grdCajas_Sheet1.Columns.Get(4).Locked = false;
            this.grdCajas_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCajas_Sheet1.Columns.Get(4).Width = 120F;
            this.grdCajas_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdCajas_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell";
            this.grdCajas_Sheet1.Rows.Default.Height = 25F;
            this.grdCajas_Sheet1.SheetCornerStyle.Parent = "CornerSeashell";
            this.grdCajas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmCapturarCajas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 366);
            this.Controls.Add(this.FrameCajas);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmCapturarCajas";
            this.Text = "Cajas de surtido - embarque";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCapturarCajas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCapturarCajas_KeyDown);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCaja_02_Final)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaja_01_Inicial)).EndInit();
            this.FrameCajas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCajas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCajas_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.NumericUpDown txtCaja_01_Inicial;
        private System.Windows.Forms.NumericUpDown txtCaja_02_Final;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private System.Windows.Forms.GroupBox FrameCajas;
        private FarPoint.Win.Spread.FpSpread grdCajas;
        private FarPoint.Win.Spread.SheetView grdCajas_Sheet1;
    }
}