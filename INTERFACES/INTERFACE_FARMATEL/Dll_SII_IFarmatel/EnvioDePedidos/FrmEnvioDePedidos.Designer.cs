namespace Dll_SII_IFarmatel.EnvioDePedidos
{
    partial class FrmEnvioDePedidos
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType49 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType50 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType51 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType52 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType53 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType9 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType54 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEnvioDePedidos));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMarcarDesmarcarTodo = new System.Windows.Forms.CheckBox();
            this.grdServiciosADomicilio = new FarPoint.Win.Spread.FpSpread();
            this.grdServiciosADomicilio_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMostrarServiciosADomicilio = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEnviarPedidos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.timerEnviandoPedidos = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdServiciosADomicilio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdServiciosADomicilio_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkMarcarDesmarcarTodo);
            this.groupBox1.Controls.Add(this.grdServiciosADomicilio);
            this.groupBox1.Location = new System.Drawing.Point(12, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1067, 581);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Servicios a domicilio";
            // 
            // chkMarcarDesmarcarTodo
            // 
            this.chkMarcarDesmarcarTodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMarcarDesmarcarTodo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcarTodo.Location = new System.Drawing.Point(749, -1);
            this.chkMarcarDesmarcarTodo.Margin = new System.Windows.Forms.Padding(4);
            this.chkMarcarDesmarcarTodo.Name = "chkMarcarDesmarcarTodo";
            this.chkMarcarDesmarcarTodo.Size = new System.Drawing.Size(301, 20);
            this.chkMarcarDesmarcarTodo.TabIndex = 1;
            this.chkMarcarDesmarcarTodo.Text = "Marcar / Desmarcar todos los servicios";
            this.chkMarcarDesmarcarTodo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcarTodo.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcarTodo.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcarTodo_CheckedChanged);
            // 
            // grdServiciosADomicilio
            // 
            this.grdServiciosADomicilio.AccessibleDescription = "grdServiciosADomicilio, Sheet1, Row 0, Column 0, ";
            this.grdServiciosADomicilio.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdServiciosADomicilio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdServiciosADomicilio.Location = new System.Drawing.Point(4, 19);
            this.grdServiciosADomicilio.Margin = new System.Windows.Forms.Padding(4);
            this.grdServiciosADomicilio.Name = "grdServiciosADomicilio";
            this.grdServiciosADomicilio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdServiciosADomicilio.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdServiciosADomicilio_Sheet1});
            this.grdServiciosADomicilio.Size = new System.Drawing.Size(1059, 558);
            this.grdServiciosADomicilio.TabIndex = 0;
            // 
            // grdServiciosADomicilio_Sheet1
            // 
            this.grdServiciosADomicilio_Sheet1.Reset();
            this.grdServiciosADomicilio_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdServiciosADomicilio_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdServiciosADomicilio_Sheet1.ColumnCount = 7;
            this.grdServiciosADomicilio_Sheet1.RowCount = 20;
            this.grdServiciosADomicilio_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Fecha de registro";
            this.grdServiciosADomicilio_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Tipo de atención";
            this.grdServiciosADomicilio_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Folio";
            this.grdServiciosADomicilio_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Beneficiario";
            this.grdServiciosADomicilio_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Domicilio";
            this.grdServiciosADomicilio_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Enviar";
            this.grdServiciosADomicilio_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Status";
            this.grdServiciosADomicilio_Sheet1.ColumnHeader.Rows.Get(0).Height = 41F;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(0).CellType = textCellType49;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(0).Label = "Fecha de registro";
            this.grdServiciosADomicilio_Sheet1.Columns.Get(0).Locked = true;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(0).Width = 80F;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(1).CellType = textCellType50;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(1).Label = "Tipo de atención";
            this.grdServiciosADomicilio_Sheet1.Columns.Get(1).Locked = true;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(1).Width = 80F;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(2).CellType = textCellType51;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(2).Label = "Folio";
            this.grdServiciosADomicilio_Sheet1.Columns.Get(2).Locked = true;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(2).Width = 80F;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(3).CellType = textCellType52;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(3).Label = "Beneficiario";
            this.grdServiciosADomicilio_Sheet1.Columns.Get(3).Locked = true;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(3).Width = 300F;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(4).CellType = textCellType53;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(4).Label = "Domicilio";
            this.grdServiciosADomicilio_Sheet1.Columns.Get(4).Locked = true;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(4).Width = 400F;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(5).CellType = checkBoxCellType9;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(5).Label = "Enviar";
            this.grdServiciosADomicilio_Sheet1.Columns.Get(5).Locked = false;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(5).Width = 80F;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(6).CellType = textCellType54;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(6).Label = "Status";
            this.grdServiciosADomicilio_Sheet1.Columns.Get(6).Locked = true;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdServiciosADomicilio_Sheet1.Columns.Get(6).Width = 150F;
            this.grdServiciosADomicilio_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdServiciosADomicilio_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator6,
            this.btnMostrarServiciosADomicilio,
            this.toolStripSeparator5,
            this.btnEnviarPedidos,
            this.toolStripSeparator});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1094, 25);
            this.toolStripBarraMenu.TabIndex = 1;
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
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnMostrarServiciosADomicilio
            // 
            this.btnMostrarServiciosADomicilio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMostrarServiciosADomicilio.Image = ((System.Drawing.Image)(resources.GetObject("btnMostrarServiciosADomicilio.Image")));
            this.btnMostrarServiciosADomicilio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMostrarServiciosADomicilio.Name = "btnMostrarServiciosADomicilio";
            this.btnMostrarServiciosADomicilio.Size = new System.Drawing.Size(23, 22);
            this.btnMostrarServiciosADomicilio.Text = "Cargar servicios a domicilio";
            this.btnMostrarServiciosADomicilio.Click += new System.EventHandler(this.btnMostrarServiciosADomicilio_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEnviarPedidos
            // 
            this.btnEnviarPedidos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEnviarPedidos.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviarPedidos.Image")));
            this.btnEnviarPedidos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEnviarPedidos.Name = "btnEnviarPedidos";
            this.btnEnviarPedidos.Size = new System.Drawing.Size(23, 22);
            this.btnEnviarPedidos.Text = "Enviar pedidos seleccionados";
            this.btnEnviarPedidos.Click += new System.EventHandler(this.btnEnviarPedidos_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // timerEnviandoPedidos
            // 
            this.timerEnviandoPedidos.Tick += new System.EventHandler(this.timerEnviandoPedidos_Tick);
            // 
            // FrmEnvioDePedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1094, 624);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmEnvioDePedidos";
            this.Text = "Listado de servicios a domicilio pendientes de enviar";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEnvioDePedidos_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEnvioDePedidos_KeyDown);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdServiciosADomicilio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdServiciosADomicilio_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnMostrarServiciosADomicilio;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnEnviarPedidos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FarPoint.Win.Spread.FpSpread grdServiciosADomicilio;
        private FarPoint.Win.Spread.SheetView grdServiciosADomicilio_Sheet1;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcarTodo;
        private System.Windows.Forms.Timer timerEnviandoPedidos;
    }
}