namespace Almacen.Pedidos
{
    partial class FrmCajasSurtidoPedidos
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
            this.FrameNumCaja = new System.Windows.Forms.GroupBox();
            this.txtIdCaja = new SC_ControlsCS.scTextBoxExt();
            this.FrameCajas = new System.Windows.Forms.GroupBox();
            this.lstCajas = new System.Windows.Forms.ListView();
            this.colCaja = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdCaja = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuCajas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnLiberarCaja = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameNumCaja.SuspendLayout();
            this.FrameCajas.SuspendLayout();
            this.menuCajas.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameNumCaja
            // 
            this.FrameNumCaja.Controls.Add(this.txtIdCaja);
            this.FrameNumCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrameNumCaja.Location = new System.Drawing.Point(16, 11);
            this.FrameNumCaja.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameNumCaja.Name = "FrameNumCaja";
            this.FrameNumCaja.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameNumCaja.Size = new System.Drawing.Size(296, 102);
            this.FrameNumCaja.TabIndex = 0;
            this.FrameNumCaja.TabStop = false;
            this.FrameNumCaja.Text = "Caja";
            // 
            // txtIdCaja
            // 
            this.txtIdCaja.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdCaja.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdCaja.Decimales = 2;
            this.txtIdCaja.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdCaja.ForeColor = System.Drawing.Color.Black;
            this.txtIdCaja.Location = new System.Drawing.Point(13, 31);
            this.txtIdCaja.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIdCaja.MaxLength = 23;
            this.txtIdCaja.Name = "txtIdCaja";
            this.txtIdCaja.PermitirApostrofo = false;
            this.txtIdCaja.PermitirNegativos = false;
            this.txtIdCaja.Size = new System.Drawing.Size(268, 53);
            this.txtIdCaja.TabIndex = 1;
            this.txtIdCaja.Text = "012345678901234567890123";
            this.txtIdCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdCaja.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdCaja_Validating);
            // 
            // FrameCajas
            // 
            this.FrameCajas.Controls.Add(this.lstCajas);
            this.FrameCajas.Location = new System.Drawing.Point(16, 116);
            this.FrameCajas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameCajas.Name = "FrameCajas";
            this.FrameCajas.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameCajas.Size = new System.Drawing.Size(296, 272);
            this.FrameCajas.TabIndex = 1;
            this.FrameCajas.TabStop = false;
            this.FrameCajas.Text = "Detalle de Cajas";
            // 
            // lstCajas
            // 
            this.lstCajas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCaja,
            this.colIdCaja});
            this.lstCajas.ContextMenuStrip = this.menuCajas;
            this.lstCajas.HideSelection = false;
            this.lstCajas.Location = new System.Drawing.Point(11, 22);
            this.lstCajas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstCajas.Name = "lstCajas";
            this.lstCajas.Size = new System.Drawing.Size(271, 233);
            this.lstCajas.TabIndex = 1;
            this.lstCajas.UseCompatibleStateImageBehavior = false;
            this.lstCajas.View = System.Windows.Forms.View.Details;
            // 
            // colCaja
            // 
            this.colCaja.Text = "Caja";
            this.colCaja.Width = 90;
            // 
            // colIdCaja
            // 
            this.colIdCaja.Text = "Id Caja";
            this.colIdCaja.Width = 105;
            // 
            // menuCajas
            // 
            this.menuCajas.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuCajas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLiberarCaja,
            this.toolStripSeparator10});
            this.menuCajas.Name = "menuPedidos";
            this.menuCajas.Size = new System.Drawing.Size(158, 34);
            // 
            // btnLiberarCaja
            // 
            this.btnLiberarCaja.Name = "btnLiberarCaja";
            this.btnLiberarCaja.Size = new System.Drawing.Size(157, 24);
            this.btnLiberarCaja.Text = "Liberar Caja";
            this.btnLiberarCaja.Click += new System.EventHandler(this.btnLiberarCaja_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(154, 6);
            // 
            // FrmCajasSurtidoPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 400);
            this.Controls.Add(this.FrameCajas);
            this.Controls.Add(this.FrameNumCaja);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmCajasSurtidoPedidos";
            this.ShowIcon = false;
            this.Text = "Asignar cajas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCajasSurtidoPedidos_Load);
            this.FrameNumCaja.ResumeLayout(false);
            this.FrameNumCaja.PerformLayout();
            this.FrameCajas.ResumeLayout(false);
            this.menuCajas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameNumCaja;
        private System.Windows.Forms.GroupBox FrameCajas;
        private System.Windows.Forms.ListView lstCajas;
        private System.Windows.Forms.ColumnHeader colCaja;
        private System.Windows.Forms.ColumnHeader colIdCaja;
        private SC_ControlsCS.scTextBoxExt txtIdCaja;
        private System.Windows.Forms.ContextMenuStrip menuCajas;
        private System.Windows.Forms.ToolStripMenuItem btnLiberarCaja;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    }
}