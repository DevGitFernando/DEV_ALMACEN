namespace DllFarmaciaSoft.CodificacionDM
{
    partial class FrmLotesSubFarmacias
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
            this.FrameLotes = new System.Windows.Forms.GroupBox();
            this.lstLotes = new System.Windows.Forms.ListView();
            this.SubFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Existencia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CodigoEAN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.IdProducto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdSubFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FrameLotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameLotes
            // 
            this.FrameLotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameLotes.Controls.Add(this.lstLotes);
            this.FrameLotes.Location = new System.Drawing.Point(12, 12);
            this.FrameLotes.Name = "FrameLotes";
            this.FrameLotes.Size = new System.Drawing.Size(650, 239);
            this.FrameLotes.TabIndex = 2;
            this.FrameLotes.TabStop = false;
            this.FrameLotes.Text = "Lotes Capturados";
            // 
            // lstLotes
            // 
            this.lstLotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLotes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdProducto,
            this.CodigoEAN,
            this.IdSubFarmacia,
            this.SubFarmacia,
            this.Lote,
            this.Existencia});
            this.lstLotes.Location = new System.Drawing.Point(10, 19);
            this.lstLotes.Name = "lstLotes";
            this.lstLotes.Size = new System.Drawing.Size(630, 209);
            this.lstLotes.TabIndex = 0;
            this.lstLotes.UseCompatibleStateImageBehavior = false;
            this.lstLotes.View = System.Windows.Forms.View.Details;
            this.lstLotes.DoubleClick += new System.EventHandler(this.lstLotes_DoubleClick);
            // 
            // SubFarmacia
            // 
            this.SubFarmacia.Text = "Sub-Farmacia";
            this.SubFarmacia.Width = 123;
            // 
            // Lote
            // 
            this.Lote.Text = "Clave Lote";
            this.Lote.Width = 160;
            // 
            // Existencia
            // 
            this.Existencia.Text = "Existencia";
            this.Existencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Existencia.Width = 100;
            // 
            // CodigoEAN
            // 
            this.CodigoEAN.Text = "Codigo EAN";
            this.CodigoEAN.Width = 169;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(0, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(674, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "Doble clic sobre el código EAN-Lote";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IdProducto
            // 
            this.IdProducto.Text = "IdProducto";
            this.IdProducto.Width = 0;
            // 
            // IdSubFarmacia
            // 
            this.IdSubFarmacia.Text = "IdSubFarmacia";
            this.IdSubFarmacia.Width = 0;
            // 
            // FrmLotesSubFarmacias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 289);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FrameLotes);
            this.Name = "FrmLotesSubFarmacias";
            this.Text = "Lote Sub-Farmacia";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmLotesSubFarmacias_Load);
            this.FrameLotes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameLotes;
        private System.Windows.Forms.ListView lstLotes;
        private System.Windows.Forms.ColumnHeader CodigoEAN;
        private System.Windows.Forms.ColumnHeader SubFarmacia;
        private System.Windows.Forms.ColumnHeader Lote;
        private System.Windows.Forms.ColumnHeader Existencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader IdProducto;
        private System.Windows.Forms.ColumnHeader IdSubFarmacia;

    }
}