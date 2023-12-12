namespace Almacen.Pedidos
{
    partial class FrmFoliosSurtidosAProcesar
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFoliosSurtidosAProcesar));
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameDetalle = new System.Windows.Forms.GroupBox();
            this.grdFolios = new FarPoint.Win.Spread.FpSpread();
            this.grdFolios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnObtenerTransferencias = new System.Windows.Forms.ToolStripButton();
            this.FrameEncabezado.SuspendLayout();
            this.FrameDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameEncabezado
            // 
            this.FrameEncabezado.Controls.Add(this.txtFolio);
            this.FrameEncabezado.Controls.Add(this.label1);
            this.FrameEncabezado.Location = new System.Drawing.Point(13, 61);
            this.FrameEncabezado.Margin = new System.Windows.Forms.Padding(4);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Padding = new System.Windows.Forms.Padding(4);
            this.FrameEncabezado.Size = new System.Drawing.Size(587, 65);
            this.FrameEncabezado.TabIndex = 1;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Datos Generales";
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.Enabled = false;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(284, 27);
            this.txtFolio.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(132, 22);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(169, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio Pedido :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDetalle
            // 
            this.FrameDetalle.Controls.Add(this.grdFolios);
            this.FrameDetalle.Location = new System.Drawing.Point(13, 128);
            this.FrameDetalle.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDetalle.Name = "FrameDetalle";
            this.FrameDetalle.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDetalle.Size = new System.Drawing.Size(587, 306);
            this.FrameDetalle.TabIndex = 2;
            this.FrameDetalle.TabStop = false;
            this.FrameDetalle.Text = "Listado de folios de surtido";
            // 
            // grdFolios
            // 
            this.grdFolios.AccessibleDescription = "grdFolios, Sheet1, Row 0, Column 0, ";
            this.grdFolios.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdFolios.HorizontalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdFolios.HorizontalScrollBar.Name = "";
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
            this.grdFolios.HorizontalScrollBar.Renderer = enhancedScrollBarRenderer1;
            this.grdFolios.HorizontalScrollBar.TabIndex = 2;
            this.grdFolios.Location = new System.Drawing.Point(12, 23);
            this.grdFolios.Margin = new System.Windows.Forms.Padding(4);
            this.grdFolios.Name = "grdFolios";
            this.grdFolios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdFolios.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdFolios_Sheet1});
            this.grdFolios.Size = new System.Drawing.Size(560, 271);
            this.grdFolios.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell;
            this.grdFolios.TabIndex = 0;
            this.grdFolios.VerticalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdFolios.VerticalScrollBar.Name = "";
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
            this.grdFolios.VerticalScrollBar.Renderer = enhancedScrollBarRenderer2;
            this.grdFolios.VerticalScrollBar.TabIndex = 3;
            // 
            // grdFolios_Sheet1
            // 
            this.grdFolios_Sheet1.Reset();
            this.grdFolios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdFolios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdFolios_Sheet1.ColumnCount = 3;
            this.grdFolios_Sheet1.RowCount = 8;
            this.grdFolios_Sheet1.Cells.Get(1, 1).Locked = false;
            this.grdFolios_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Folio";
            this.grdFolios_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Fecha Registro";
            this.grdFolios_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Procesar";
            this.grdFolios_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell";
            this.grdFolios_Sheet1.ColumnHeader.Rows.Get(0).Height = 23F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 15;
            this.grdFolios_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdFolios_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(0).Label = "Folio";
            this.grdFolios_Sheet1.Columns.Get(0).Locked = true;
            this.grdFolios_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(0).Width = 97F;
            this.grdFolios_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdFolios_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(1).Label = "Fecha Registro";
            this.grdFolios_Sheet1.Columns.Get(1).Locked = true;
            this.grdFolios_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(1).Width = 127F;
            this.grdFolios_Sheet1.Columns.Get(2).CellType = checkBoxCellType1;
            this.grdFolios_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(2).Label = "Procesar";
            this.grdFolios_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(2).Width = 98F;
            this.grdFolios_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdFolios_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell";
            this.grdFolios_Sheet1.SheetCornerStyle.Parent = "CornerSeashell";
            this.grdFolios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnObtenerTransferencias});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(615, 58);
            this.toolStripBarraMenu.TabIndex = 3;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnObtenerTransferencias
            // 
            this.btnObtenerTransferencias.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnObtenerTransferencias.Image = ((System.Drawing.Image)(resources.GetObject("btnObtenerTransferencias.Image")));
            this.btnObtenerTransferencias.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnObtenerTransferencias.Name = "btnObtenerTransferencias";
            this.btnObtenerTransferencias.Size = new System.Drawing.Size(54, 55);
            this.btnObtenerTransferencias.Text = "Descargar información";
            this.btnObtenerTransferencias.Click += new System.EventHandler(this.btnObtenerTransferencias_Click);
            // 
            // FrmFoliosSurtidosAProcesar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 449);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameDetalle);
            this.Controls.Add(this.FrameEncabezado);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmFoliosSurtidosAProcesar";
            this.ShowIcon = false;
            this.Text = "Generar documento concentrado";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFoliosSurtidosAProcesar_Load);
            this.FrameEncabezado.ResumeLayout(false);
            this.FrameEncabezado.PerformLayout();
            this.FrameDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameEncabezado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox FrameDetalle;
        private FarPoint.Win.Spread.FpSpread grdFolios;
        private FarPoint.Win.Spread.SheetView grdFolios_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnObtenerTransferencias;
    }
}