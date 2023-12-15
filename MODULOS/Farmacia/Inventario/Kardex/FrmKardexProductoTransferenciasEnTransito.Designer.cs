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
            this.components = new System.ComponentModel.Container();
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
            this.FrameDetalle.Location = new System.Drawing.Point(15, 7);
            this.FrameDetalle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDetalle.Name = "FrameDetalle";
            this.FrameDetalle.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDetalle.Size = new System.Drawing.Size(932, 460);
            this.FrameDetalle.TabIndex = 6;
            this.FrameDetalle.TabStop = false;
            this.FrameDetalle.Text = "Detalle";
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(187, 406);
            this.scLabelExt2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(223, 37);
            this.scLabelExt2.TabIndex = 6;
            this.scLabelExt2.Text = "Traspasos : ";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTransferencias
            // 
            this.lblTransferencias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransferencias.Location = new System.Drawing.Point(413, 406);
            this.lblTransferencias.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTransferencias.MostrarToolTip = false;
            this.lblTransferencias.Name = "lblTransferencias";
            this.lblTransferencias.Size = new System.Drawing.Size(136, 37);
            this.lblTransferencias.TabIndex = 5;
            this.lblTransferencias.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(557, 406);
            this.scLabelExt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt1.MostrarToolTip = true;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(133, 37);
            this.scLabelExt1.TabIndex = 4;
            this.scLabelExt1.Text = "Total de Piezas : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalPiezas
            // 
            this.lblTotalPiezas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPiezas.Location = new System.Drawing.Point(699, 406);
            this.lblTotalPiezas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPiezas.MostrarToolTip = false;
            this.lblTotalPiezas.Name = "lblTotalPiezas";
            this.lblTotalPiezas.Size = new System.Drawing.Size(221, 37);
            this.lblTotalPiezas.TabIndex = 3;
            this.lblTotalPiezas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listMovimientos
            // 
            this.listMovimientos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listMovimientos.HideSelection = false;
            this.listMovimientos.Location = new System.Drawing.Point(13, 23);
            this.listMovimientos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listMovimientos.Name = "listMovimientos";
            this.listMovimientos.Size = new System.Drawing.Size(905, 373);
            this.listMovimientos.TabIndex = 2;
            this.listMovimientos.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "payaso";
            // 
            // FrmKardexProductoTransferenciasEnTransito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 478);
            this.Controls.Add(this.FrameDetalle);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmKardexProductoTransferenciasEnTransito";
            this.ShowIcon = false;
            this.Text = "Traspasos en Tránsito";
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