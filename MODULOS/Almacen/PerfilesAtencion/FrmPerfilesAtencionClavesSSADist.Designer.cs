namespace Almacen.PerfilesAtencion
{
    partial class FrmPerfilesAtencionClavesSSADist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPerfilesAtencionClavesSSADist));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDatosGral = new System.Windows.Forms.GroupBox();
            this.lblPerfil = new System.Windows.Forms.Label();
            this.txtPerfilAtencion = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.FramePerfiles = new System.Windows.Forms.GroupBox();
            this.twGrupos = new System.Windows.Forms.TreeView();
            this.mnGrupos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.lwUsuarios = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnClaves = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bntBuscarClaves = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatosGral.SuspendLayout();
            this.FramePerfiles.SuspendLayout();
            this.mnGrupos.SuspendLayout();
            this.FrameClaves.SuspendLayout();
            this.mnClaves.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1000, 25);
            this.toolStripBarraMenu.TabIndex = 7;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameDatosGral
            // 
            this.FrameDatosGral.Controls.Add(this.lblPerfil);
            this.FrameDatosGral.Controls.Add(this.txtPerfilAtencion);
            this.FrameDatosGral.Controls.Add(this.label2);
            this.FrameDatosGral.Location = new System.Drawing.Point(12, 24);
            this.FrameDatosGral.Name = "FrameDatosGral";
            this.FrameDatosGral.Size = new System.Drawing.Size(976, 60);
            this.FrameDatosGral.TabIndex = 8;
            this.FrameDatosGral.TabStop = false;
            this.FrameDatosGral.Text = "Datos de Perfiles de Atención";
            // 
            // lblPerfil
            // 
            this.lblPerfil.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPerfil.Location = new System.Drawing.Point(178, 22);
            this.lblPerfil.Name = "lblPerfil";
            this.lblPerfil.Size = new System.Drawing.Size(788, 21);
            this.lblPerfil.TabIndex = 39;
            this.lblPerfil.Text = "Perfil :";
            this.lblPerfil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPerfilAtencion
            // 
            this.txtPerfilAtencion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPerfilAtencion.Decimales = 2;
            this.txtPerfilAtencion.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPerfilAtencion.ForeColor = System.Drawing.Color.Black;
            this.txtPerfilAtencion.Location = new System.Drawing.Point(101, 23);
            this.txtPerfilAtencion.MaxLength = 10;
            this.txtPerfilAtencion.Name = "txtPerfilAtencion";
            this.txtPerfilAtencion.PermitirApostrofo = false;
            this.txtPerfilAtencion.PermitirNegativos = false;
            this.txtPerfilAtencion.Size = new System.Drawing.Size(71, 20);
            this.txtPerfilAtencion.TabIndex = 32;
            this.txtPerfilAtencion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPerfilAtencion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPerfilAtencion_KeyDown);
            this.txtPerfilAtencion.Validating += new System.ComponentModel.CancelEventHandler(this.txtPerfilAtencion_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "Perfil Atención :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FramePerfiles
            // 
            this.FramePerfiles.Controls.Add(this.twGrupos);
            this.FramePerfiles.Location = new System.Drawing.Point(12, 86);
            this.FramePerfiles.Name = "FramePerfiles";
            this.FramePerfiles.Size = new System.Drawing.Size(340, 425);
            this.FramePerfiles.TabIndex = 9;
            this.FramePerfiles.TabStop = false;
            this.FramePerfiles.Text = "Niveles de Atención";
            // 
            // twGrupos
            // 
            this.twGrupos.AllowDrop = true;
            this.twGrupos.ContextMenuStrip = this.mnGrupos;
            this.twGrupos.FullRowSelect = true;
            this.twGrupos.Location = new System.Drawing.Point(10, 16);
            this.twGrupos.Name = "twGrupos";
            this.twGrupos.PathSeparator = "|";
            this.twGrupos.ShowNodeToolTips = true;
            this.twGrupos.Size = new System.Drawing.Size(320, 400);
            this.twGrupos.TabIndex = 0;
            this.twGrupos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twGrupos_AfterSelect);
            this.twGrupos.DragDrop += new System.Windows.Forms.DragEventHandler(this.twGrupos_DragDrop);
            this.twGrupos.DragEnter += new System.Windows.Forms.DragEventHandler(this.twGrupos_DragEnter);
            // 
            // mnGrupos
            // 
            this.mnGrupos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.eliminarToolStripMenuItem,
            this.toolStripSeparator2,
            this.actualizarToolStripMenuItem,
            this.toolStripSeparator4});
            this.mnGrupos.Name = "mnGrupos";
            this.mnGrupos.ShowImageMargin = false;
            this.mnGrupos.Size = new System.Drawing.Size(143, 66);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(139, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(139, 6);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.actualizarToolStripMenuItem.Text = "Actualizar perfiles";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(139, 6);
            // 
            // FrameClaves
            // 
            this.FrameClaves.Controls.Add(this.lwUsuarios);
            this.FrameClaves.Location = new System.Drawing.Point(357, 86);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(631, 425);
            this.FrameClaves.TabIndex = 10;
            this.FrameClaves.TabStop = false;
            this.FrameClaves.Text = "Claves SSA";
            // 
            // lwUsuarios
            // 
            this.lwUsuarios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwUsuarios.ContextMenuStrip = this.mnClaves;
            this.lwUsuarios.FullRowSelect = true;
            this.lwUsuarios.Location = new System.Drawing.Point(10, 16);
            this.lwUsuarios.Name = "lwUsuarios";
            this.lwUsuarios.ShowItemToolTips = true;
            this.lwUsuarios.Size = new System.Drawing.Size(611, 400);
            this.lwUsuarios.TabIndex = 0;
            this.lwUsuarios.UseCompatibleStateImageBehavior = false;
            this.lwUsuarios.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lwUsuarios_ItemDrag);
            // 
            // mnClaves
            // 
            this.mnClaves.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bntBuscarClaves});
            this.mnClaves.Name = "mnClaves";
            this.mnClaves.Size = new System.Drawing.Size(145, 26);
            // 
            // bntBuscarClaves
            // 
            this.bntBuscarClaves.Name = "bntBuscarClaves";
            this.bntBuscarClaves.Size = new System.Drawing.Size(152, 22);
            this.bntBuscarClaves.Text = "Buscar claves";
            this.bntBuscarClaves.Click += new System.EventHandler(this.bntBuscarClaves_Click);
            // 
            // FrmPerfilesAtencionClavesSSADist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 521);
            this.Controls.Add(this.FramePerfiles);
            this.Controls.Add(this.FrameClaves);
            this.Controls.Add(this.FrameDatosGral);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmPerfilesAtencionClavesSSADist";
            this.Text = "Asignar Claves a Perfiles de Atencion";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPerfilesAtencionClavesSSADist_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatosGral.ResumeLayout(false);
            this.FrameDatosGral.PerformLayout();
            this.FramePerfiles.ResumeLayout(false);
            this.mnGrupos.ResumeLayout(false);
            this.FrameClaves.ResumeLayout(false);
            this.mnClaves.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.GroupBox FrameDatosGral;
        private SC_ControlsCS.scTextBoxExt txtPerfilAtencion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FramePerfiles;
        private System.Windows.Forms.TreeView twGrupos;
        private System.Windows.Forms.GroupBox FrameClaves;
        private System.Windows.Forms.ListView lwUsuarios;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip mnClaves;
        private System.Windows.Forms.ToolStripMenuItem bntBuscarClaves;
        private System.Windows.Forms.ContextMenuStrip mnGrupos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label lblPerfil;
    }
}