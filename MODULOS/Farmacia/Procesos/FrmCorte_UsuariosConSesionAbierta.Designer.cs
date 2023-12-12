namespace Farmacia.Procesos
{
    partial class FrmCorte_UsuariosConSesionAbierta
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdUsuarios = new FarPoint.Win.Spread.FpSpread();
            this.grdUsuarios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsuarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsuarios_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grdUsuarios);
            this.groupBox1.Location = new System.Drawing.Point(6, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(669, 320);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Usuarios con sesiones abiertas";
            // 
            // grdUsuarios
            // 
            this.grdUsuarios.AccessibleDescription = "grdUsuarios, Sheet1, Row 0, Column 0, ";
            this.grdUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdUsuarios.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdUsuarios.Location = new System.Drawing.Point(9, 19);
            this.grdUsuarios.Name = "grdUsuarios";
            this.grdUsuarios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdUsuarios.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdUsuarios_Sheet1});
            this.grdUsuarios.Size = new System.Drawing.Size(651, 295);
            this.grdUsuarios.TabIndex = 1;
            this.grdUsuarios.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdUsuarios_CellDoubleClick);
            // 
            // grdUsuarios_Sheet1
            // 
            this.grdUsuarios_Sheet1.Reset();
            this.grdUsuarios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdUsuarios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdUsuarios_Sheet1.ColumnCount = 3;
            this.grdUsuarios_Sheet1.RowCount = 10;
            this.grdUsuarios_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Personal";
            this.grdUsuarios_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.grdUsuarios_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Login";
            this.grdUsuarios_Sheet1.Columns.Get(0).CellType = textCellType7;
            this.grdUsuarios_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUsuarios_Sheet1.Columns.Get(0).Label = "Personal";
            this.grdUsuarios_Sheet1.Columns.Get(0).Locked = true;
            this.grdUsuarios_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUsuarios_Sheet1.Columns.Get(0).Width = 78F;
            this.grdUsuarios_Sheet1.Columns.Get(1).CellType = textCellType8;
            this.grdUsuarios_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUsuarios_Sheet1.Columns.Get(1).Label = "Nombre";
            this.grdUsuarios_Sheet1.Columns.Get(1).Locked = true;
            this.grdUsuarios_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUsuarios_Sheet1.Columns.Get(1).Width = 317F;
            this.grdUsuarios_Sheet1.Columns.Get(2).CellType = textCellType9;
            this.grdUsuarios_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUsuarios_Sheet1.Columns.Get(2).Label = "Login";
            this.grdUsuarios_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUsuarios_Sheet1.Columns.Get(2).Width = 104F;
            this.grdUsuarios_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdUsuarios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(657, 38);
            this.label1.TabIndex = 5;
            this.label1.Text = "Los siguientes usuarios tienen sesion de corte abierta. Cierre estas sesiones en " +
    "la opción Cortes Parciales para continuar. ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(669, 56);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 387);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(684, 24);
            this.lblMensajes.TabIndex = 11;
            this.lblMensajes.Text = " Doble clic sobre el Personal para generar el Corte Parcial";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmCorte_UsuariosConSesionAbierta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 411);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCorte_UsuariosConSesionAbierta";
            this.Text = "Corte del Día ó Cambio de Día";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCorte_UsuariosConSesionAbierta_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUsuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsuarios_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdUsuarios;
        private FarPoint.Win.Spread.SheetView grdUsuarios_Sheet1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblMensajes;
    }
}