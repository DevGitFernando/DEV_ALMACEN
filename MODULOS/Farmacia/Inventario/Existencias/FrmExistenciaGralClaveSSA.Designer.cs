namespace Farmacia.Inventario
{
    partial class FrmExistenciaGralClaveSSA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExistenciaGralClaveSSA));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblDescripcionSal = new SC_ControlsCS.scLabelExt();
            this.lstClaves = new System.Windows.Forms.ListView();
            this.ClaveInterna = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClaveSSA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Desc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Exist_Actual = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Exist_Transito = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Exist_Surtido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Exist_Total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkConUbicaciones = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoRptSinExist = new System.Windows.Forms.RadioButton();
            this.rdoRptTodos = new System.Windows.Forms.RadioButton();
            this.rdoRptConExist = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoRptGeneral = new System.Windows.Forms.RadioButton();
            this.rdoRptDetallado = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator4,
            this.btnImprimir,
            this.toolStripSeparator3,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1445, 58);
            this.toolStripBarraMenu.TabIndex = 7;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(54, 55);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 58);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(54, 55);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(12, 58);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(54, 55);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(12, 58);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(54, 55);
            this.btnExportarExcel.Text = "toolStripButton1";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.lblDescripcionSal);
            this.groupBox2.Controls.Add(this.lstClaves);
            this.groupBox2.Controls.Add(this.chkConUbicaciones);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Location = new System.Drawing.Point(16, 62);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1421, 650);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1076, 609);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 28);
            this.label3.TabIndex = 46;
            this.label3.Text = "Existencia Total :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(1225, 609);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(184, 28);
            this.lblTotal.TabIndex = 45;
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescripcionSal
            // 
            this.lblDescripcionSal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcionSal.Location = new System.Drawing.Point(9, 609);
            this.lblDescripcionSal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcionSal.MostrarToolTip = false;
            this.lblDescripcionSal.Name = "lblDescripcionSal";
            this.lblDescripcionSal.Size = new System.Drawing.Size(1035, 28);
            this.lblDescripcionSal.TabIndex = 44;
            this.lblDescripcionSal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstClaves
            // 
            this.lstClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstClaves.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ClaveInterna,
            this.ClaveSSA,
            this.Desc,
            this.Exist_Actual,
            this.Exist_Transito,
            this.Exist_Surtido,
            this.Exist_Total});
            this.lstClaves.HideSelection = false;
            this.lstClaves.Location = new System.Drawing.Point(12, 78);
            this.lstClaves.Margin = new System.Windows.Forms.Padding(4);
            this.lstClaves.Name = "lstClaves";
            this.lstClaves.Size = new System.Drawing.Size(1396, 523);
            this.lstClaves.TabIndex = 8;
            this.lstClaves.UseCompatibleStateImageBehavior = false;
            this.lstClaves.View = System.Windows.Forms.View.Details;
            this.lstClaves.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstClaves_ItemSelectionChanged);
            this.lstClaves.DoubleClick += new System.EventHandler(this.lstClaves_DoubleClick);
            // 
            // ClaveInterna
            // 
            this.ClaveInterna.Text = "Clave Interna";
            this.ClaveInterna.Width = 80;
            // 
            // ClaveSSA
            // 
            this.ClaveSSA.Text = "Clave SSA";
            this.ClaveSSA.Width = 110;
            // 
            // Desc
            // 
            this.Desc.Text = "Descripción Clave";
            this.Desc.Width = 310;
            // 
            // Exist_Actual
            // 
            this.Exist_Actual.Text = "Existencia Actual";
            this.Exist_Actual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Exist_Actual.Width = 110;
            // 
            // Exist_Transito
            // 
            this.Exist_Transito.Text = "Existencia en Tránsito";
            this.Exist_Transito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Exist_Transito.Width = 130;
            // 
            // Exist_Surtido
            // 
            this.Exist_Surtido.Text = "Existencia en Surtido";
            this.Exist_Surtido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Exist_Surtido.Width = 120;
            // 
            // Exist_Total
            // 
            this.Exist_Total.Text = "Existencia Total";
            this.Exist_Total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Exist_Total.Width = 110;
            // 
            // chkConUbicaciones
            // 
            this.chkConUbicaciones.Enabled = false;
            this.chkConUbicaciones.Location = new System.Drawing.Point(321, 229);
            this.chkConUbicaciones.Margin = new System.Windows.Forms.Padding(4);
            this.chkConUbicaciones.Name = "chkConUbicaciones";
            this.chkConUbicaciones.Size = new System.Drawing.Size(127, 30);
            this.chkConUbicaciones.TabIndex = 6;
            this.chkConUbicaciones.Text = "Con ubicaciones";
            this.chkConUbicaciones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConUbicaciones.UseVisualStyleBackColor = true;
            this.chkConUbicaciones.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoRptSinExist);
            this.groupBox3.Controls.Add(this.rdoRptTodos);
            this.groupBox3.Controls.Add(this.rdoRptConExist);
            this.groupBox3.Location = new System.Drawing.Point(589, 18);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(820, 54);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de Existencias";
            // 
            // rdoRptSinExist
            // 
            this.rdoRptSinExist.Location = new System.Drawing.Point(563, 23);
            this.rdoRptSinExist.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRptSinExist.Name = "rdoRptSinExist";
            this.rdoRptSinExist.Size = new System.Drawing.Size(136, 21);
            this.rdoRptSinExist.TabIndex = 6;
            this.rdoRptSinExist.Text = "Sin Existencia";
            this.rdoRptSinExist.UseVisualStyleBackColor = true;
            // 
            // rdoRptTodos
            // 
            this.rdoRptTodos.Checked = true;
            this.rdoRptTodos.Location = new System.Drawing.Point(120, 23);
            this.rdoRptTodos.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRptTodos.Name = "rdoRptTodos";
            this.rdoRptTodos.Size = new System.Drawing.Size(136, 21);
            this.rdoRptTodos.TabIndex = 4;
            this.rdoRptTodos.TabStop = true;
            this.rdoRptTodos.Text = "Todos";
            this.rdoRptTodos.UseVisualStyleBackColor = true;
            // 
            // rdoRptConExist
            // 
            this.rdoRptConExist.Location = new System.Drawing.Point(341, 23);
            this.rdoRptConExist.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRptConExist.Name = "rdoRptConExist";
            this.rdoRptConExist.Size = new System.Drawing.Size(136, 21);
            this.rdoRptConExist.TabIndex = 5;
            this.rdoRptConExist.Text = "Con Existencia";
            this.rdoRptConExist.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoRptGeneral);
            this.groupBox1.Controls.Add(this.rdoRptDetallado);
            this.groupBox1.Location = new System.Drawing.Point(9, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(571, 54);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formato Informe";
            // 
            // rdoRptGeneral
            // 
            this.rdoRptGeneral.Checked = true;
            this.rdoRptGeneral.Location = new System.Drawing.Point(123, 23);
            this.rdoRptGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRptGeneral.Name = "rdoRptGeneral";
            this.rdoRptGeneral.Size = new System.Drawing.Size(109, 21);
            this.rdoRptGeneral.TabIndex = 4;
            this.rdoRptGeneral.TabStop = true;
            this.rdoRptGeneral.Text = "General";
            this.rdoRptGeneral.UseVisualStyleBackColor = true;
            // 
            // rdoRptDetallado
            // 
            this.rdoRptDetallado.Location = new System.Drawing.Point(340, 23);
            this.rdoRptDetallado.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRptDetallado.Name = "rdoRptDetallado";
            this.rdoRptDetallado.Size = new System.Drawing.Size(109, 21);
            this.rdoRptDetallado.TabIndex = 5;
            this.rdoRptDetallado.Text = "Detallado";
            this.rdoRptDetallado.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.SteelBlue;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(0, 722);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1445, 30);
            this.label11.TabIndex = 10;
            this.label11.Text = " ( F7 )      -----  Seleccionar Fuentes Financiamiento  -----";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmExistenciaGralClaveSSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1445, 752);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmExistenciaGralClaveSSA";
            this.ShowIcon = false;
            this.Text = "Existencia General por Clave SSA";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmExistenciaPorClaveSSA_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmExistenciaGralClaveSSA_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoRptDetallado;
        private System.Windows.Forms.RadioButton rdoRptGeneral;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoRptTodos;
        private System.Windows.Forms.RadioButton rdoRptConExist;
        private System.Windows.Forms.RadioButton rdoRptSinExist;
        private System.Windows.Forms.CheckBox chkConUbicaciones;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ListView lstClaves;
        private System.Windows.Forms.ColumnHeader ClaveInterna;
        private System.Windows.Forms.ColumnHeader ClaveSSA;
        private System.Windows.Forms.ColumnHeader Desc;
        private System.Windows.Forms.ColumnHeader Exist_Actual;
        private System.Windows.Forms.ColumnHeader Exist_Transito;
        private System.Windows.Forms.ColumnHeader Exist_Total;
        private SC_ControlsCS.scLabelExt lblDescripcionSal;
        private System.Windows.Forms.ColumnHeader Exist_Surtido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}