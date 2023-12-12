namespace Dll_SII_IRFID.Demonio
{
    partial class FrmTAGS_Invalidos
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
            this.FrameTagsInvalidos = new System.Windows.Forms.GroupBox();
            this.lstTagsInvalidos = new System.Windows.Forms.ListView();
            this.colReader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTAG = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tmGPO = new System.Windows.Forms.Timer(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.FrameTagsInvalidos.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameTagsInvalidos
            // 
            this.FrameTagsInvalidos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameTagsInvalidos.Controls.Add(this.lstTagsInvalidos);
            this.FrameTagsInvalidos.Location = new System.Drawing.Point(12, 5);
            this.FrameTagsInvalidos.Name = "FrameTagsInvalidos";
            this.FrameTagsInvalidos.Size = new System.Drawing.Size(948, 292);
            this.FrameTagsInvalidos.TabIndex = 0;
            this.FrameTagsInvalidos.TabStop = false;
            this.FrameTagsInvalidos.Text = "Listado de tags";
            // 
            // lstTagsInvalidos
            // 
            this.lstTagsInvalidos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTagsInvalidos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colReader,
            this.colTAG});
            this.lstTagsInvalidos.Location = new System.Drawing.Point(10, 20);
            this.lstTagsInvalidos.Name = "lstTagsInvalidos";
            this.lstTagsInvalidos.Size = new System.Drawing.Size(930, 261);
            this.lstTagsInvalidos.TabIndex = 0;
            this.lstTagsInvalidos.UseCompatibleStateImageBehavior = false;
            this.lstTagsInvalidos.View = System.Windows.Forms.View.Details;
            // 
            // colReader
            // 
            this.colReader.Text = "Lector";
            this.colReader.Width = 180;
            // 
            // colTAG
            // 
            this.colTAG.Text = "Identificador RFID";
            this.colTAG.Width = 415;
            // 
            // tmGPO
            // 
            this.tmGPO.Tick += new System.EventHandler(this.tmGPO_Tick);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(12, 303);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(948, 35);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "[F5] Reset listado de tags";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FrmTAGS_Invalidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 346);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.FrameTagsInvalidos);
            this.Name = "FrmTAGS_Invalidos";
            this.Text = "Listado de Tags invalidos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTAGS_Invalidos_FormClosing);
            this.Load += new System.EventHandler(this.FrmTAGS_Invalidos_Load);
            this.Shown += new System.EventHandler(this.FrmTAGS_Invalidos_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmTAGS_Invalidos_KeyDown);
            this.FrameTagsInvalidos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameTagsInvalidos;
        private System.Windows.Forms.ListView lstTagsInvalidos;
        private System.Windows.Forms.ColumnHeader colTAG;
        private System.Windows.Forms.Timer tmGPO;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ColumnHeader colReader;
    }
}