namespace Farmacia.Inventario
{
    partial class FrmKardexProductoTransferenciasEnTransito
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
            this.FrameDetalle = new System.Windows.Forms.GroupBox();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.lblTransferencias = new SC_ControlsCS.scLabelExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.lblTotalPiezas = new SC_ControlsCS.scLabelExt();
            this.listMovimientos = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FrameDetalle.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameDetalle
            // 
            this.FrameDetalle.Controls.Add(this.scLabelExt2);
            this.FrameDetalle.Controls.Add(this.lblTransferencias);
            this.FrameDetalle.Controls.Add(this.scLabelExt1);
            this.FrameDetalle.Controls.Add(this.lblTotalPiezas);
            this.FrameDetalle.Controls.Add(this.listMovimientos);
            this.FrameDetalle.Location = new System.Drawing.Point(11, 6);
            this.FrameDetalle.Name = "FrameDetalle";
            this.FrameDetalle.Size = new System.Drawing.Size(699, 374);
            this.FrameDetalle.TabIndex = 6;
            this.FrameDetalle.TabStop = false;
            this.FrameDetalle.Text = "Detalle";
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(140, 330);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(167, 30);
            this.scLabelExt2.TabIndex = 6;
            this.scLabelExt2.Text = "Número de Transferencias : ";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTransferencias
            // 
            this.lblTransferencias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTransferencias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransferencias.Location = new System.Drawing.Point(310, 330);
            this.lblTransferencias.MostrarToolTip = false;
            this.lblTransferencias.Name = "lblTransferencias";
            this.lblTransferencias.Size = new System.Drawing.Size(102, 30);
            this.lblTransferencias.TabIndex = 5;
            this.lblTransferencias.Text = "scLabelExt1";
            this.lblTransferencias.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(418, 330);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(100, 30);
            this.scLabelExt1.TabIndex = 4;
            this.scLabelExt1.Text = "Total de Piezas : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalPiezas
            // 
            this.lblTotalPiezas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalPiezas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPiezas.Location = new System.Drawing.Point(524, 330);
            this.lblTotalPiezas.MostrarToolTip = false;
            this.lblTotalPiezas.Name = "lblTotalPiezas";
            this.lblTotalPiezas.Size = new System.Drawing.Size(166, 30);
            this.lblTotalPiezas.TabIndex = 3;
            this.lblTotalPiezas.Text = "scLabelExt1";
            this.lblTotalPiezas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listMovimientos
            // 
            this.listMovimientos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listMovimientos.Location = new System.Drawing.Point(10, 19);
            this.listMovimientos.Name = "listMovimientos";
            this.listMovimientos.Size = new System.Drawing.Size(680, 304);
            this.listMovimientos.TabIndex = 2;
            this.listMovimientos.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "payaso";
            // 
            // FrmKardexProductoTransferenciasEnTransito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 388);
            this.Controls.Add(this.FrameDetalle);
            this.Name = "FrmKardexProductoTransferenciasEnTransito";
            this.Text = "Transferencias en Tránsito";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FrameDetalle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameDetalle;
        private System.Windows.Forms.ListView listMovimientos;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private SC_ControlsCS.scLabelExt lblTotalPiezas;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private SC_ControlsCS.scLabelExt lblTransferencias;
    }
}