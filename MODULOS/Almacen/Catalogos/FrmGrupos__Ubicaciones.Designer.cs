namespace Almacen.Catalogos
{
    partial class FrmGrupos__Ubicaciones
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
            this.FrameGrupos = new System.Windows.Forms.GroupBox();
            this.twGrupos = new System.Windows.Forms.TreeView();
            this.mnGrupos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Eliminar_toolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.FrameUsuarios = new System.Windows.Forms.GroupBox();
            this.lwUsuarios = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FrameGrupos.SuspendLayout();
            this.mnGrupos.SuspendLayout();
            this.FrameUsuarios.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameGrupos
            // 
            this.FrameGrupos.Controls.Add(this.twGrupos);
            this.FrameGrupos.Location = new System.Drawing.Point(12, 14);
            this.FrameGrupos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameGrupos.Name = "FrameGrupos";
            this.FrameGrupos.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameGrupos.Size = new System.Drawing.Size(520, 559);
            this.FrameGrupos.TabIndex = 2;
            this.FrameGrupos.TabStop = false;
            this.FrameGrupos.Text = "Grupos Ubicaciones";
            // 
            // twGrupos
            // 
            this.twGrupos.AllowDrop = true;
            this.twGrupos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twGrupos.ContextMenuStrip = this.mnGrupos;
            this.twGrupos.FullRowSelect = true;
            this.twGrupos.Location = new System.Drawing.Point(8, 23);
            this.twGrupos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.twGrupos.Name = "twGrupos";
            this.twGrupos.PathSeparator = "|";
            this.twGrupos.ShowNodeToolTips = true;
            this.twGrupos.Size = new System.Drawing.Size(503, 527);
            this.twGrupos.TabIndex = 0;
            this.twGrupos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twGrupos_AfterSelect);
            this.twGrupos.DragDrop += new System.Windows.Forms.DragEventHandler(this.twGrupos_DragDrop);
            this.twGrupos.DragEnter += new System.Windows.Forms.DragEventHandler(this.twGrupos_DragEnter);
            // 
            // mnGrupos
            // 
            this.mnGrupos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnGrupos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Eliminar_toolStrip});
            this.mnGrupos.Name = "mnGrupos";
            this.mnGrupos.Size = new System.Drawing.Size(133, 28);
            // 
            // Eliminar_toolStrip
            // 
            this.Eliminar_toolStrip.Name = "Eliminar_toolStrip";
            this.Eliminar_toolStrip.Size = new System.Drawing.Size(132, 24);
            this.Eliminar_toolStrip.Text = "Eliminar";
            this.Eliminar_toolStrip.Click += new System.EventHandler(this.Eliminar_toolStrip_Click);
            // 
            // FrameUsuarios
            // 
            this.FrameUsuarios.Controls.Add(this.lwUsuarios);
            this.FrameUsuarios.Location = new System.Drawing.Point(535, 12);
            this.FrameUsuarios.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameUsuarios.Name = "FrameUsuarios";
            this.FrameUsuarios.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameUsuarios.Size = new System.Drawing.Size(520, 559);
            this.FrameUsuarios.TabIndex = 3;
            this.FrameUsuarios.TabStop = false;
            this.FrameUsuarios.Text = "Ubicaciones";
            // 
            // lwUsuarios
            // 
            this.lwUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwUsuarios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwUsuarios.FullRowSelect = true;
            this.lwUsuarios.HideSelection = false;
            this.lwUsuarios.Location = new System.Drawing.Point(8, 23);
            this.lwUsuarios.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lwUsuarios.Name = "lwUsuarios";
            this.lwUsuarios.ShowItemToolTips = true;
            this.lwUsuarios.Size = new System.Drawing.Size(503, 527);
            this.lwUsuarios.TabIndex = 0;
            this.lwUsuarios.UseCompatibleStateImageBehavior = false;
            this.lwUsuarios.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lwUsuarios_ItemDrag);
            // 
            // FrmGrupos__Ubicaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 583);
            this.Controls.Add(this.FrameUsuarios);
            this.Controls.Add(this.FrameGrupos);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmGrupos__Ubicaciones";
            this.ShowIcon = false;
            this.Text = "Configuración Grupos Ubicaciones";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRotacion_Claves_Load);
            this.FrameGrupos.ResumeLayout(false);
            this.mnGrupos.ResumeLayout(false);
            this.FrameUsuarios.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameGrupos;
        private System.Windows.Forms.TreeView twGrupos;
        private System.Windows.Forms.GroupBox FrameUsuarios;
        private System.Windows.Forms.ListView lwUsuarios;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip mnGrupos;
        private System.Windows.Forms.ToolStripMenuItem Eliminar_toolStrip;
    }
}