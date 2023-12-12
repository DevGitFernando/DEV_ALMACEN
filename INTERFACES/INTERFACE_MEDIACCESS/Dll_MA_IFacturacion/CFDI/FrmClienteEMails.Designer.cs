namespace Dll_MA_IFacturacion.CFDI
{
    partial class FrmClienteEMails
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClienteEMails));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdCorreos = new FarPoint.Win.Spread.FpSpread();
            this.grdCorreos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCustomerSendMail = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameEnvioCorreoNoRegistrado = new System.Windows.Forms.GroupBox();
            this.txtCorreo = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCorreos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCorreos_Sheet1)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.FrameEnvioCorreoNoRegistrado.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdCorreos);
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 187);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "E-mails";
            // 
            // grdCorreos
            // 
            this.grdCorreos.AccessibleDescription = "grdCorreos, Sheet1, Row 0, Column 0, ";
            this.grdCorreos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdCorreos.Location = new System.Drawing.Point(10, 18);
            this.grdCorreos.Name = "grdCorreos";
            this.grdCorreos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdCorreos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdCorreos_Sheet1});
            this.grdCorreos.Size = new System.Drawing.Size(577, 162);
            this.grdCorreos.TabIndex = 0;
            // 
            // grdCorreos_Sheet1
            // 
            this.grdCorreos_Sheet1.Reset();
            this.grdCorreos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdCorreos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdCorreos_Sheet1.ColumnCount = 3;
            this.grdCorreos_Sheet1.RowCount = 6;
            this.grdCorreos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Tipo";
            this.grdCorreos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Email";
            this.grdCorreos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Enviar";
            this.grdCorreos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdCorreos_Sheet1.Columns.Get(0).Label = "Tipo";
            this.grdCorreos_Sheet1.Columns.Get(0).Locked = true;
            this.grdCorreos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCorreos_Sheet1.Columns.Get(0).Width = 140F;
            this.grdCorreos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdCorreos_Sheet1.Columns.Get(1).Label = "Email";
            this.grdCorreos_Sheet1.Columns.Get(1).Locked = true;
            this.grdCorreos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCorreos_Sheet1.Columns.Get(1).Width = 320F;
            this.grdCorreos_Sheet1.Columns.Get(2).CellType = checkBoxCellType1;
            this.grdCorreos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCorreos_Sheet1.Columns.Get(2).Label = "Enviar";
            this.grdCorreos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCorreos_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdCorreos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStrip3
            // 
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator6,
            this.btnCustomerSendMail,
            this.toolStripSeparator9});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(616, 39);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 39);
            // 
            // btnCustomerSendMail
            // 
            this.btnCustomerSendMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCustomerSendMail.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomerSendMail.Image")));
            this.btnCustomerSendMail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCustomerSendMail.Name = "btnCustomerSendMail";
            this.btnCustomerSendMail.Size = new System.Drawing.Size(36, 36);
            this.btnCustomerSendMail.Text = "Enviar por E-Mail";
            this.btnCustomerSendMail.Click += new System.EventHandler(this.btnCustomerSendMail_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 39);
            // 
            // FrameEnvioCorreoNoRegistrado
            // 
            this.FrameEnvioCorreoNoRegistrado.Controls.Add(this.txtCorreo);
            this.FrameEnvioCorreoNoRegistrado.Controls.Add(this.label7);
            this.FrameEnvioCorreoNoRegistrado.Location = new System.Drawing.Point(12, 231);
            this.FrameEnvioCorreoNoRegistrado.Name = "FrameEnvioCorreoNoRegistrado";
            this.FrameEnvioCorreoNoRegistrado.Size = new System.Drawing.Size(596, 53);
            this.FrameEnvioCorreoNoRegistrado.TabIndex = 2;
            this.FrameEnvioCorreoNoRegistrado.TabStop = false;
            this.FrameEnvioCorreoNoRegistrado.Text = "Envio a un correo no registrado";
            // 
            // txtCorreo
            // 
            this.txtCorreo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCorreo.Decimales = 2;
            this.txtCorreo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCorreo.ForeColor = System.Drawing.Color.Black;
            this.txtCorreo.Location = new System.Drawing.Point(78, 19);
            this.txtCorreo.MaxLength = 100;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.PermitirApostrofo = false;
            this.txtCorreo.PermitirNegativos = false;
            this.txtCorreo.Size = new System.Drawing.Size(494, 20);
            this.txtCorreo.TabIndex = 0;
            this.txtCorreo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(11, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Correo :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmClienteEMails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 293);
            this.Controls.Add(this.FrameEnvioCorreoNoRegistrado);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmClienteEMails";
            this.Text = "Correos Electrónicos";
            this.Load += new System.EventHandler(this.FrmClienteEMails_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCorreos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCorreos_Sheet1)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.FrameEnvioCorreoNoRegistrado.ResumeLayout(false);
            this.FrameEnvioCorreoNoRegistrado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdCorreos;
        private FarPoint.Win.Spread.SheetView grdCorreos_Sheet1;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnCustomerSendMail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.GroupBox FrameEnvioCorreoNoRegistrado;
        private SC_ControlsCS.scTextBoxExt txtCorreo;
        private System.Windows.Forms.Label label7;
    }
}