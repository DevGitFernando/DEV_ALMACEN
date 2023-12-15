namespace Farmacia.Inventario
{
    partial class FrmExistenciaPorCodigoInterno
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExistenciaPorCodigoInterno));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lstEAN = new System.Windows.Forms.ListView();
            this.Codigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Exist_Actual = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Exist_Transito = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Exist_Surtido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Exist_Total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoRptSinExist = new System.Windows.Forms.RadioButton();
            this.rdoRptTodos = new System.Windows.Forms.RadioButton();
            this.rdoRptConExist = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.lblDescripcionClave = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.lstEAN);
            this.groupBox2.Location = new System.Drawing.Point(12, 308);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1288, 432);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(943, 395);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 28);
            this.label3.TabIndex = 50;
            this.label3.Text = "Existencia Total :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(1092, 395);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(184, 28);
            this.lblTotal.TabIndex = 49;
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstEAN
            // 
            this.lstEAN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstEAN.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Codigo,
            this.Exist_Actual,
            this.Exist_Transito,
            this.Exist_Surtido,
            this.Exist_Total});
            this.lstEAN.HideSelection = false;
            this.lstEAN.Location = new System.Drawing.Point(12, 23);
            this.lstEAN.Margin = new System.Windows.Forms.Padding(4);
            this.lstEAN.Name = "lstEAN";
            this.lstEAN.Size = new System.Drawing.Size(1263, 367);
            this.lstEAN.TabIndex = 0;
            this.lstEAN.UseCompatibleStateImageBehavior = false;
            this.lstEAN.View = System.Windows.Forms.View.Details;
            this.lstEAN.DoubleClick += new System.EventHandler(this.lstEAN_DoubleClick);
            // 
            // Codigo
            // 
            this.Codigo.Text = "Codigo EAN";
            this.Codigo.Width = 110;
            // 
            // Exist_Actual
            // 
            this.Exist_Actual.Text = "Existencia Actual";
            this.Exist_Actual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Exist_Actual.Width = 105;
            // 
            // Exist_Transito
            // 
            this.Exist_Transito.Text = "Existencia en Tránsito";
            this.Exist_Transito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Exist_Transito.Width = 120;
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
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1312, 58);
            this.toolStripBarraMenu.TabIndex = 0;
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
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 4);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(12, 4);
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
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.SteelBlue;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(0, 746);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1312, 30);
            this.label11.TabIndex = 14;
            this.label11.Text = "( F7 )  -----  Seleccionar Fuentes Financiamiento  -----";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.rdoRptSinExist);
            this.groupBox3.Controls.Add(this.rdoRptTodos);
            this.groupBox3.Controls.Add(this.rdoRptConExist);
            this.groupBox3.Location = new System.Drawing.Point(12, 68);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1288, 43);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de Existencias";
            // 
            // rdoRptSinExist
            // 
            this.rdoRptSinExist.Location = new System.Drawing.Point(843, 13);
            this.rdoRptSinExist.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRptSinExist.Name = "rdoRptSinExist";
            this.rdoRptSinExist.Size = new System.Drawing.Size(160, 23);
            this.rdoRptSinExist.TabIndex = 2;
            this.rdoRptSinExist.Text = "Sin Existencia";
            this.rdoRptSinExist.UseVisualStyleBackColor = true;
            // 
            // rdoRptTodos
            // 
            this.rdoRptTodos.Checked = true;
            this.rdoRptTodos.Location = new System.Drawing.Point(285, 13);
            this.rdoRptTodos.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRptTodos.Name = "rdoRptTodos";
            this.rdoRptTodos.Size = new System.Drawing.Size(160, 23);
            this.rdoRptTodos.TabIndex = 0;
            this.rdoRptTodos.TabStop = true;
            this.rdoRptTodos.Text = "Todos";
            this.rdoRptTodos.UseVisualStyleBackColor = true;
            // 
            // rdoRptConExist
            // 
            this.rdoRptConExist.Location = new System.Drawing.Point(564, 13);
            this.rdoRptConExist.Margin = new System.Windows.Forms.Padding(4);
            this.rdoRptConExist.Name = "rdoRptConExist";
            this.rdoRptConExist.Size = new System.Drawing.Size(160, 23);
            this.rdoRptConExist.TabIndex = 1;
            this.rdoRptConExist.Text = "Con Existencia";
            this.rdoRptConExist.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtClaveSSA);
            this.groupBox1.Controls.Add(this.lblDescripcionClave);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 114);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1291, 187);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(129, 23);
            this.txtClaveSSA.Margin = new System.Windows.Forms.Padding(4);
            this.txtClaveSSA.MaxLength = 20;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(199, 22);
            this.txtClaveSSA.TabIndex = 0;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDescripcionClave
            // 
            this.lblDescripcionClave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcionClave.Location = new System.Drawing.Point(129, 50);
            this.lblDescripcionClave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcionClave.Name = "lblDescripcionClave";
            this.lblDescripcionClave.Size = new System.Drawing.Size(1147, 43);
            this.lblDescripcionClave.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Clave SSA :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.Location = new System.Drawing.Point(129, 132);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(1147, 43);
            this.lblDescripcion.TabIndex = 5;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(129, 103);
            this.txtId.Margin = new System.Windows.Forms.Padding(4);
            this.txtId.MaxLength = 8;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(199, 22);
            this.txtId.TabIndex = 1;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 106);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Código Interno :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmExistenciaPorCodigoInterno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 776);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmExistenciaPorCodigoInterno";
            this.ShowIcon = false;
            this.Text = "Existencia por Código Interno";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmExistenciaPorCodigoInterno_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmExistenciaPorCodigoInterno_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListView lstEAN;
        private System.Windows.Forms.ColumnHeader Codigo;
        private System.Windows.Forms.ColumnHeader Exist_Actual;
        private System.Windows.Forms.ColumnHeader Exist_Transito;
        private System.Windows.Forms.ColumnHeader Exist_Total;
        private System.Windows.Forms.ColumnHeader Exist_Surtido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoRptSinExist;
        private System.Windows.Forms.RadioButton rdoRptTodos;
        private System.Windows.Forms.RadioButton rdoRptConExist;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label lblDescripcionClave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDescripcion;
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
    }
}