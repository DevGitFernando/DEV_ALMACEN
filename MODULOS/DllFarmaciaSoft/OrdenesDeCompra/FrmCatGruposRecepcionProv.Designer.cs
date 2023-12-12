namespace DllFarmaciaSoft.OrdenesDeCompra
{
    partial class FrmCatGruposRecepcionProv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCatGruposRecepcionProv));
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType11 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType12 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType6 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkEsRecepcionOC = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtGrupo = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new SC_ControlsCS.scTextBoxExt();
            this.FrameResultados = new System.Windows.Forms.GroupBox();
            this.grdMotivos = new FarPoint.Win.Spread.FpSpread();
            this.grdMotivos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameResultados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMotivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMotivos_Sheet1)).BeginInit();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(734, 25);
            this.toolStripBarraMenu.TabIndex = 7;
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
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkEsRecepcionOC);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.txtGrupo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Location = new System.Drawing.Point(10, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 136);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Grupo";
            // 
            // chkEsRecepcionOC
            // 
            this.chkEsRecepcionOC.Location = new System.Drawing.Point(92, 102);
            this.chkEsRecepcionOC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkEsRecepcionOC.Name = "chkEsRecepcionOC";
            this.chkEsRecepcionOC.Size = new System.Drawing.Size(235, 22);
            this.chkEsRecepcionOC.TabIndex = 11;
            this.chkEsRecepcionOC.Text = "Es para recepción de orden de compra";
            this.chkEsRecepcionOC.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(181, 24);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(81, 13);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "CANCELADO";
            this.lblStatus.Visible = false;
            // 
            // txtGrupo
            // 
            this.txtGrupo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrupo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtGrupo.Decimales = 2;
            this.txtGrupo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtGrupo.ForeColor = System.Drawing.Color.Black;
            this.txtGrupo.Location = new System.Drawing.Point(92, 22);
            this.txtGrupo.MaxLength = 3;
            this.txtGrupo.Name = "txtGrupo";
            this.txtGrupo.PermitirApostrofo = false;
            this.txtGrupo.PermitirNegativos = false;
            this.txtGrupo.Size = new System.Drawing.Size(80, 20);
            this.txtGrupo.TabIndex = 1;
            this.txtGrupo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGrupo.Validating += new System.ComponentModel.CancelEventHandler(this.txtGrupo_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Grupo :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Descripción :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDescripcion.Decimales = 2;
            this.txtDescripcion.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDescripcion.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Location = new System.Drawing.Point(92, 46);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.PermitirApostrofo = false;
            this.txtDescripcion.PermitirNegativos = false;
            this.txtDescripcion.Size = new System.Drawing.Size(612, 52);
            this.txtDescripcion.TabIndex = 2;
            // 
            // FrameResultados
            // 
            this.FrameResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameResultados.Controls.Add(this.grdMotivos);
            this.FrameResultados.Location = new System.Drawing.Point(10, 167);
            this.FrameResultados.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FrameResultados.Name = "FrameResultados";
            this.FrameResultados.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FrameResultados.Size = new System.Drawing.Size(714, 284);
            this.FrameResultados.TabIndex = 9;
            this.FrameResultados.TabStop = false;
            this.FrameResultados.Text = "Escala de resultados";
            // 
            // grdMotivos
            // 
            this.grdMotivos.AccessibleDescription = "grdMotivos, Sheet1, Row 0, Column 0, ";
            this.grdMotivos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMotivos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdMotivos.Location = new System.Drawing.Point(10, 18);
            this.grdMotivos.Name = "grdMotivos";
            this.grdMotivos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdMotivos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdMotivos_Sheet1});
            this.grdMotivos.Size = new System.Drawing.Size(694, 257);
            this.grdMotivos.TabIndex = 1;
            // 
            // grdMotivos_Sheet1
            // 
            this.grdMotivos_Sheet1.Reset();
            this.grdMotivos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdMotivos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdMotivos_Sheet1.ColumnCount = 5;
            this.grdMotivos_Sheet1.RowCount = 10;
            this.grdMotivos_Sheet1.Cells.Get(1, 1).Locked = false;
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre del resultado";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Rango inicial";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Rango final";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Activo";
            this.grdMotivos_Sheet1.ColumnHeader.Rows.Get(0).Height = 33F;
            textCellType11.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType11.MaxLength = 100;
            this.grdMotivos_Sheet1.Columns.Get(0).CellType = textCellType11;
            this.grdMotivos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(0).Label = "Clave";
            this.grdMotivos_Sheet1.Columns.Get(0).Locked = false;
            this.grdMotivos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(0).Width = 83F;
            this.grdMotivos_Sheet1.Columns.Get(1).CellType = textCellType12;
            this.grdMotivos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMotivos_Sheet1.Columns.Get(1).Label = "Nombre del resultado";
            this.grdMotivos_Sheet1.Columns.Get(1).Locked = false;
            this.grdMotivos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.grdMotivos_Sheet1.Columns.Get(1).Width = 320F;
            numberCellType11.DecimalPlaces = 0;
            numberCellType11.MaximumValue = 100D;
            numberCellType11.MinimumValue = 0D;
            this.grdMotivos_Sheet1.Columns.Get(2).CellType = numberCellType11;
            this.grdMotivos_Sheet1.Columns.Get(2).Label = "Rango inicial";
            this.grdMotivos_Sheet1.Columns.Get(2).Width = 90F;
            numberCellType12.DecimalPlaces = 0;
            numberCellType12.MaximumValue = 100D;
            numberCellType12.MinimumValue = 0D;
            this.grdMotivos_Sheet1.Columns.Get(3).CellType = numberCellType12;
            this.grdMotivos_Sheet1.Columns.Get(3).Label = "Rango final";
            this.grdMotivos_Sheet1.Columns.Get(3).Width = 90F;
            this.grdMotivos_Sheet1.Columns.Get(4).CellType = checkBoxCellType6;
            this.grdMotivos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(4).Label = "Activo";
            this.grdMotivos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdMotivos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmCatGruposRecepcionProv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.FrameResultados);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmCatGruposRecepcionProv";
            this.Text = "Grupos para evaluación a proveedores";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCatGruposRecepcionProv_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameResultados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMotivos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMotivos_Sheet1)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblStatus;
        private SC_ControlsCS.scTextBoxExt txtGrupo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtDescripcion;
        private System.Windows.Forms.CheckBox chkEsRecepcionOC;
        private System.Windows.Forms.GroupBox FrameResultados;
        private FarPoint.Win.Spread.FpSpread grdMotivos;
        private FarPoint.Win.Spread.SheetView grdMotivos_Sheet1;
    }
}