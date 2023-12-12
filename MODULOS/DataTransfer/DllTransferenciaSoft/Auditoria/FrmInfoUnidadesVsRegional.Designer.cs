namespace DllTransferenciaSoft.Auditoria
{
    partial class FrmInfoUnidadesVsRegional
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInfoUnidadesVsRegional));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDiferencias = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPorcentaje = new System.Windows.Forms.Label();
            this.grdCatalogos = new FarPoint.Win.Spread.FpSpread();
            this.menuAuditoria = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.porcentajesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diferenciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grdCatalogos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.grdExistencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnObtenerInformacion = new System.Windows.Forms.ToolStripButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.tmEjecucionesUnidad = new System.Windows.Forms.Timer(this.components);
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCatalogos)).BeginInit();
            this.menuAuditoria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCatalogos_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.lblDiferencias);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblPorcentaje);
            this.groupBox3.Controls.Add(this.grdCatalogos);
            this.groupBox3.Location = new System.Drawing.Point(8, 84);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(949, 422);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Catalogos";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(381, 385);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Total de Diferencias :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiferencias
            // 
            this.lblDiferencias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDiferencias.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiferencias.Location = new System.Drawing.Point(521, 383);
            this.lblDiferencias.Name = "lblDiferencias";
            this.lblDiferencias.Size = new System.Drawing.Size(120, 27);
            this.lblDiferencias.TabIndex = 7;
            this.lblDiferencias.Text = "label3";
            this.lblDiferencias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(661, 384);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 23);
            this.label6.TabIndex = 6;
            this.label6.Text = "Porcentaje General :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPorcentaje
            // 
            this.lblPorcentaje.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPorcentaje.Location = new System.Drawing.Point(801, 382);
            this.lblPorcentaje.Name = "lblPorcentaje";
            this.lblPorcentaje.Size = new System.Drawing.Size(120, 27);
            this.lblPorcentaje.TabIndex = 5;
            this.lblPorcentaje.Text = "label3";
            this.lblPorcentaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdCatalogos
            // 
            this.grdCatalogos.AccessibleDescription = "grdCatalogos, Sheet1, Row 0, Column 0, ";
            this.grdCatalogos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdCatalogos.ContextMenuStrip = this.menuAuditoria;
            this.grdCatalogos.Location = new System.Drawing.Point(14, 18);
            this.grdCatalogos.Name = "grdCatalogos";
            this.grdCatalogos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdCatalogos.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.grdCatalogos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdCatalogos_Sheet1});
            this.grdCatalogos.Size = new System.Drawing.Size(926, 362);
            this.grdCatalogos.TabIndex = 1;
            // 
            // menuAuditoria
            // 
            this.menuAuditoria.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.porcentajesToolStripMenuItem,
            this.diferenciasToolStripMenuItem});
            this.menuAuditoria.Name = "menuAuditoria";
            this.menuAuditoria.Size = new System.Drawing.Size(136, 48);
            // 
            // porcentajesToolStripMenuItem
            // 
            this.porcentajesToolStripMenuItem.Name = "porcentajesToolStripMenuItem";
            this.porcentajesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.porcentajesToolStripMenuItem.Text = "Porcentajes";
            this.porcentajesToolStripMenuItem.Click += new System.EventHandler(this.porcentajesToolStripMenuItem_Click);
            // 
            // diferenciasToolStripMenuItem
            // 
            this.diferenciasToolStripMenuItem.Name = "diferenciasToolStripMenuItem";
            this.diferenciasToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.diferenciasToolStripMenuItem.Text = "Diferencias";
            this.diferenciasToolStripMenuItem.Click += new System.EventHandler(this.diferenciasToolStripMenuItem_Click);
            // 
            // grdCatalogos_Sheet1
            // 
            this.grdCatalogos_Sheet1.Reset();
            this.grdCatalogos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdCatalogos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdCatalogos_Sheet1.ColumnCount = 7;
            this.grdCatalogos_Sheet1.RowCount = 15;
            this.grdCatalogos_Sheet1.Cells.Get(0, 2).Value = 3;
            this.grdCatalogos_Sheet1.Cells.Get(0, 3).Value = 1;
            this.grdCatalogos_Sheet1.Cells.Get(0, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(0, 5).Value = 33.333333333333329;
            this.grdCatalogos_Sheet1.Cells.Get(0, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(0, 6).Value = 33.333333333333329;
            this.grdCatalogos_Sheet1.Cells.Get(1, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(1, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(1, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(1, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(2, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(2, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(2, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(2, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(3, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(3, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(3, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(3, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(4, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(4, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(4, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(4, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(5, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(5, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(5, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(5, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(6, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(6, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(6, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(6, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(7, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(7, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(7, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(7, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(8, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(8, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(8, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(8, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(9, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(9, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(9, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(9, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(10, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(10, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(10, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(10, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(11, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(11, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(11, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(11, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(12, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(12, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(12, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(12, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(13, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(13, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(13, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(13, 6).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(14, 5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(14, 5).Value = 0;
            this.grdCatalogos_Sheet1.Cells.Get(14, 6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Cells.Get(14, 6).Value = 0;
            this.grdCatalogos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Nombre";
            this.grdCatalogos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Procesar";
            this.grdCatalogos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Registros Unidad";
            this.grdCatalogos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Registros Regional";
            this.grdCatalogos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Diferencia";
            this.grdCatalogos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Porcentaje";
            this.grdCatalogos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "((D1/C1)*100)";
            this.grdCatalogos_Sheet1.ColumnHeader.Rows.Get(0).Height = 41F;
            this.grdCatalogos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdCatalogos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdCatalogos_Sheet1.Columns.Get(0).Label = "Nombre";
            this.grdCatalogos_Sheet1.Columns.Get(0).Locked = true;
            this.grdCatalogos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(0).Width = 440F;
            this.grdCatalogos_Sheet1.Columns.Get(1).CellType = checkBoxCellType1;
            this.grdCatalogos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(1).Label = "Procesar";
            this.grdCatalogos_Sheet1.Columns.Get(1).Width = 70F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000;
            numberCellType1.MinimumValue = 0;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdCatalogos_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grdCatalogos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdCatalogos_Sheet1.Columns.Get(2).Label = "Registros Unidad";
            this.grdCatalogos_Sheet1.Columns.Get(2).Locked = false;
            this.grdCatalogos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(2).Width = 100F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000;
            numberCellType2.MinimumValue = 0;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdCatalogos_Sheet1.Columns.Get(3).CellType = numberCellType2;
            this.grdCatalogos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdCatalogos_Sheet1.Columns.Get(3).Label = "Registros Regional";
            this.grdCatalogos_Sheet1.Columns.Get(3).Locked = false;
            this.grdCatalogos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(3).Width = 100F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 10000000;
            numberCellType3.MinimumValue = 0;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdCatalogos_Sheet1.Columns.Get(4).CellType = numberCellType3;
            this.grdCatalogos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdCatalogos_Sheet1.Columns.Get(4).Label = "Diferencia";
            this.grdCatalogos_Sheet1.Columns.Get(4).Locked = true;
            this.grdCatalogos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(4).Width = 80F;
            numberCellType4.DecimalPlaces = 4;
            numberCellType4.MinimumValue = 0;
            this.grdCatalogos_Sheet1.Columns.Get(5).CellType = numberCellType4;
            this.grdCatalogos_Sheet1.Columns.Get(5).Formula = "IF((RC[-2]+RC[-3])>0,((RC[-2]/RC[-3])*100),0)";
            this.grdCatalogos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(5).Label = "Porcentaje";
            this.grdCatalogos_Sheet1.Columns.Get(5).Locked = true;
            this.grdCatalogos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(5).Width = 80F;
            numberCellType5.DecimalPlaces = 0;
            numberCellType5.MaximumValue = 10000000;
            numberCellType5.MinimumValue = 0;
            this.grdCatalogos_Sheet1.Columns.Get(6).CellType = numberCellType5;
            this.grdCatalogos_Sheet1.Columns.Get(6).Formula = "IF((RC[-3]+RC[-4])>0,((RC[-3]/RC[-4])*100),0)";
            this.grdCatalogos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(6).Label = "((D1/C1)*100)";
            this.grdCatalogos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(6).Visible = false;
            this.grdCatalogos_Sheet1.Columns.Get(6).Width = 190F;
            this.grdCatalogos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdCatalogos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // grdExistencia_Sheet1
            // 
            this.grdExistencia_Sheet1.Reset();
            this.grdExistencia_Sheet1.SheetName = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cboFarmacias);
            this.groupBox2.Controls.Add(this.chkTodos);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboEstados);
            this.groupBox2.Location = new System.Drawing.Point(8, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(949, 52);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Estados y Farmacias";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(394, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Farmacia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.Location = new System.Drawing.Point(456, 21);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(290, 21);
            this.cboFarmacias.TabIndex = 24;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // chkTodos
            // 
            this.chkTodos.Location = new System.Drawing.Point(782, 22);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(154, 24);
            this.chkTodos.TabIndex = 4;
            this.chkTodos.Text = "Marcar / Desmarcar Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Estado :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.Location = new System.Drawing.Point(83, 21);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(290, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnObtenerInformacion});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(966, 25);
            this.toolStripBarraMenu.TabIndex = 2;
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
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Obtener información Regional";
            this.btnEjecutar.ToolTipText = "Obtener información Regional";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnObtenerInformacion
            // 
            this.btnObtenerInformacion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnObtenerInformacion.Image = ((System.Drawing.Image)(resources.GetObject("btnObtenerInformacion.Image")));
            this.btnObtenerInformacion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnObtenerInformacion.Name = "btnObtenerInformacion";
            this.btnObtenerInformacion.Size = new System.Drawing.Size(23, 22);
            this.btnObtenerInformacion.Text = "Obtener información Unidad";
            this.btnObtenerInformacion.Click += new System.EventHandler(this.btnObtenerTransferencias_Click);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // tmEjecucionesUnidad
            // 
            this.tmEjecucionesUnidad.Interval = 500;
            this.tmEjecucionesUnidad.Tick += new System.EventHandler(this.tmEjecucionesUnidad_Tick);
            // 
            // FrmInfoUnidadesVsRegional
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 513);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmInfoUnidadesVsRegional";
            this.Text = "Auditar Informacion de Farmacia en Regional";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmInfoUnidadesVsRegional_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCatalogos)).EndInit();
            this.menuAuditoria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCatalogos_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.SheetView grdExistencia_Sheet1;
        private System.Windows.Forms.GroupBox groupBox2;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.CheckBox chkTodos;
        private FarPoint.Win.Spread.FpSpread grdCatalogos;
        private FarPoint.Win.Spread.SheetView grdCatalogos_Sheet1;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.ToolStripButton btnObtenerInformacion;
        private System.Windows.Forms.Timer tmEjecucionesUnidad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPorcentaje;
        private System.Windows.Forms.ContextMenuStrip menuAuditoria;
        private System.Windows.Forms.ToolStripMenuItem porcentajesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diferenciasToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDiferencias;
    }
}