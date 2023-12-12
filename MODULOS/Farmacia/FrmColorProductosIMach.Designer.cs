namespace Farmacia
{
    partial class FrmColorProductosIMach
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmColorProductosIMach));
            this.lblColor = new System.Windows.Forms.Label();
            this.cboColores = new SC_ControlsCS.scComboBoxExt();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnVale = new System.Windows.Forms.ToolStripButton();
            this.grdColor = new FarPoint.Win.Spread.FpSpread();
            this.grdColor_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdColor_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblColor
            // 
            this.lblColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblColor.Location = new System.Drawing.Point(9, 60);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(468, 84);
            this.lblColor.TabIndex = 1;
            this.lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboColores
            // 
            this.cboColores.BackColorEnabled = System.Drawing.Color.White;
            this.cboColores.Data = "";
            this.cboColores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColores.Filtro = " 1 = 1";
            this.cboColores.FormattingEnabled = true;
            this.cboColores.ListaItemsBusqueda = 20;
            this.cboColores.Location = new System.Drawing.Point(9, 32);
            this.cboColores.MostrarToolTip = false;
            this.cboColores.Name = "cboColores";
            this.cboColores.Size = new System.Drawing.Size(468, 21);
            this.cboColores.TabIndex = 0;
            this.cboColores.SelectedIndexChanged += new System.EventHandler(this.cboColores_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(275, 281);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(380, 281);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.btnImprimir,
            this.btnVale});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(489, 25);
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
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
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
            // 
            // btnVale
            // 
            this.btnVale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnVale.Enabled = false;
            this.btnVale.Image = ((System.Drawing.Image)(resources.GetObject("btnVale.Image")));
            this.btnVale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVale.Name = "btnVale";
            this.btnVale.Size = new System.Drawing.Size(23, 22);
            this.btnVale.Text = "&Imprimir (CTRL + V)";
            this.btnVale.Visible = false;
            // 
            // grdColor
            // 
            this.grdColor.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.grdColor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdColor.Location = new System.Drawing.Point(9, 147);
            this.grdColor.Name = "grdColor";
            this.grdColor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdColor.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdColor_Sheet1});
            this.grdColor.Size = new System.Drawing.Size(468, 128);
            this.grdColor.TabIndex = 8;
            // 
            // grdColor_Sheet1
            // 
            this.grdColor_Sheet1.Reset();
            this.grdColor_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdColor_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdColor_Sheet1.ColumnCount = 1;
            this.grdColor_Sheet1.RowCount = 1;
            this.grdColor_Sheet1.Columns.Get(0).Width = 400F;
            this.grdColor_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdColor_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmColorProductosIMach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 317);
            this.Controls.Add(this.grdColor);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.cboColores);
            this.Name = "FrmColorProductosIMach";
            this.Text = "Seleccionar Color para Productos de Mach4";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTestColor_FormClosing);
            this.Load += new System.EventHandler(this.FrmTestColor_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdColor_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SC_ControlsCS.scComboBoxExt cboColores;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnVale;
        private FarPoint.Win.Spread.FpSpread grdColor;
        private FarPoint.Win.Spread.SheetView grdColor_Sheet1;
    }
}