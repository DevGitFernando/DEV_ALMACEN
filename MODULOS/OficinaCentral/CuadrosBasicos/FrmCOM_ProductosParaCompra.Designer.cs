namespace OficinaCentral.CuadrosBasicos
{
    partial class FrmCOM_ProductosParaCompra
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
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstProductos = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.twClaves = new System.Windows.Forms.TreeView();
            this.mnClaves = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClaves_CargarLista = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClaves_BuscarClaves = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCargarProductosAsigandos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCargarProductosRelacionados = new System.Windows.Forms.ToolStripMenuItem();
            this.FrameClaves.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mnClaves.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameClaves
            // 
            this.FrameClaves.Controls.Add(this.lblEstado);
            this.FrameClaves.Controls.Add(this.twClaves);
            this.FrameClaves.Controls.Add(this.cboEstados);
            this.FrameClaves.Location = new System.Drawing.Point(12, 4);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(442, 500);
            this.FrameClaves.TabIndex = 3;
            this.FrameClaves.TabStop = false;
            this.FrameClaves.Text = "Estados y Claves SSA";
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(27, 21);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(53, 19);
            this.lblEstado.TabIndex = 19;
            this.lblEstado.Text = "Estado :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(81, 19);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(351, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstProductos);
            this.groupBox1.Location = new System.Drawing.Point(460, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 500);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Productos comerciales";
            // 
            // lstProductos
            // 
            this.lstProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProductos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lstProductos.FullRowSelect = true;
            this.lstProductos.Location = new System.Drawing.Point(10, 16);
            this.lstProductos.Name = "lstProductos";
            this.lstProductos.ShowItemToolTips = true;
            this.lstProductos.Size = new System.Drawing.Size(618, 474);
            this.lstProductos.TabIndex = 0;
            this.lstProductos.UseCompatibleStateImageBehavior = false;
            // 
            // twClaves
            // 
            this.twClaves.AllowDrop = true;
            this.twClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.twClaves.ContextMenuStrip = this.mnClaves;
            this.twClaves.FullRowSelect = true;
            this.twClaves.Location = new System.Drawing.Point(10, 46);
            this.twClaves.Name = "twClaves";
            this.twClaves.PathSeparator = "|";
            this.twClaves.ShowNodeToolTips = true;
            this.twClaves.Size = new System.Drawing.Size(422, 444);
            this.twClaves.TabIndex = 1;
            this.twClaves.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twClaves_AfterSelect);
            // 
            // mnClaves
            // 
            this.mnClaves.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.btnClaves_CargarLista,
            this.toolStripSeparator9,
            this.btnClaves_BuscarClaves,
            this.toolStripSeparator1,
            this.toolStripSeparator3,
            this.toolStripSeparator2,
            this.btnCargarProductosAsigandos,
            this.toolStripSeparator8,
            this.btnCargarProductosRelacionados});
            this.mnClaves.Name = "mnGrupos";
            this.mnClaves.ShowImageMargin = false;
            this.mnClaves.Size = new System.Drawing.Size(252, 150);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(248, 6);
            // 
            // btnClaves_CargarLista
            // 
            this.btnClaves_CargarLista.Name = "btnClaves_CargarLista";
            this.btnClaves_CargarLista.Size = new System.Drawing.Size(251, 22);
            this.btnClaves_CargarLista.Text = "Cargar Claves Asignadas";
            this.btnClaves_CargarLista.Click += new System.EventHandler(this.btnClaves_CargarLista_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(248, 6);
            // 
            // btnClaves_BuscarClaves
            // 
            this.btnClaves_BuscarClaves.Name = "btnClaves_BuscarClaves";
            this.btnClaves_BuscarClaves.Size = new System.Drawing.Size(251, 22);
            this.btnClaves_BuscarClaves.Text = "Buscar Claves";
            this.btnClaves_BuscarClaves.Click += new System.EventHandler(this.btnClaves_BuscarClaves_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(248, 6);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(248, 6);
            // 
            // btnCargarProductosAsigandos
            // 
            this.btnCargarProductosAsigandos.Name = "btnCargarProductosAsigandos";
            this.btnCargarProductosAsigandos.Size = new System.Drawing.Size(251, 22);
            this.btnCargarProductosAsigandos.Text = "Cargar productos asignados";
            this.btnCargarProductosAsigandos.Click += new System.EventHandler(this.btnCargarProductosAsigandos_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(248, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(248, 6);
            // 
            // btnCargarProductosRelacionados
            // 
            this.btnCargarProductosRelacionados.Name = "btnCargarProductosRelacionados";
            this.btnCargarProductosRelacionados.Size = new System.Drawing.Size(251, 22);
            this.btnCargarProductosRelacionados.Text = "Cargar lista de productos relacionados";
            this.btnCargarProductosRelacionados.Click += new System.EventHandler(this.btnCargarProductosRelacionados_Click);
            // 
            // FrmCOM_ProductosParaCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 511);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameClaves);
            this.Name = "FrmCOM_ProductosParaCompra";
            this.Text = "Perfil de compras de productos por estado";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCOM_ProductosParaCompra_Load);
            this.FrameClaves.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.mnClaves.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameClaves;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstProductos;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TreeView twClaves;
        private System.Windows.Forms.ContextMenuStrip mnClaves;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem btnClaves_CargarLista;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem btnClaves_BuscarClaves;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem btnCargarProductosAsigandos;
        private System.Windows.Forms.ToolStripMenuItem btnCargarProductosRelacionados;
    }
}