namespace Almacen.Pedidos
{
    partial class FrmAgregarExistenciaClaveDistribuccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgregarExistenciaClaveDistribuccion));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarExistencia = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblExisTransito = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblExistencia = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblContPte = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPresentacion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDescripcion = new SC_ControlsCS.scLabelExt();
            this.lblIdClaveSSA = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.scTextBoxExt1 = new SC_ControlsCS.scTextBoxExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGenerarExistencia,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(836, 27);
            this.toolStripBarraMenu.TabIndex = 2;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(29, 24);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnGenerarExistencia
            // 
            this.btnGenerarExistencia.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarExistencia.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarExistencia.Image")));
            this.btnGenerarExistencia.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarExistencia.Name = "btnGenerarExistencia";
            this.btnGenerarExistencia.Size = new System.Drawing.Size(29, 24);
            this.btnGenerarExistencia.Text = "Procesar existencia para distribución";
            this.btnGenerarExistencia.Click += new System.EventHandler(this.btnGenerarExistencia_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblExisTransito);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblExistencia);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblContPte);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblPresentacion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.lblIdClaveSSA);
            this.groupBox1.Controls.Add(this.txtClaveSSA);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(812, 241);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Clave SSA";
            // 
            // lblExisTransito
            // 
            this.lblExisTransito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblExisTransito.Location = new System.Drawing.Point(460, 197);
            this.lblExisTransito.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExisTransito.Name = "lblExisTransito";
            this.lblExisTransito.Size = new System.Drawing.Size(176, 25);
            this.lblExisTransito.TabIndex = 14;
            this.lblExisTransito.Text = "label2";
            this.lblExisTransito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(293, 202);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(159, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Existencia en Tránsito :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblExistencia
            // 
            this.lblExistencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblExistencia.Location = new System.Drawing.Point(115, 197);
            this.lblExistencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExistencia.Name = "lblExistencia";
            this.lblExistencia.Size = new System.Drawing.Size(168, 25);
            this.lblExistencia.TabIndex = 12;
            this.lblExistencia.Text = "label2";
            this.lblExistencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 202);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Existencia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContPte
            // 
            this.lblContPte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContPte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContPte.Location = new System.Drawing.Point(460, 161);
            this.lblContPte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContPte.Name = "lblContPte";
            this.lblContPte.Size = new System.Drawing.Size(176, 25);
            this.lblContPte.TabIndex = 10;
            this.lblContPte.Text = "label2";
            this.lblContPte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(341, 166);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Cont. paquete :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPresentacion
            // 
            this.lblPresentacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPresentacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentacion.Location = new System.Drawing.Point(115, 161);
            this.lblPresentacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.Size = new System.Drawing.Size(168, 25);
            this.lblPresentacion.TabIndex = 8;
            this.lblPresentacion.Text = "label2";
            this.lblPresentacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 166);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Presentación :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(115, 57);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcion.MostrarToolTip = false;
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(668, 92);
            this.lblDescripcion.TabIndex = 6;
            this.lblDescripcion.Text = "Descripcion";
            // 
            // lblIdClaveSSA
            // 
            this.lblIdClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdClaveSSA.Location = new System.Drawing.Point(323, 23);
            this.lblIdClaveSSA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIdClaveSSA.Name = "lblIdClaveSSA";
            this.lblIdClaveSSA.Size = new System.Drawing.Size(120, 25);
            this.lblIdClaveSSA.TabIndex = 5;
            this.lblIdClaveSSA.Text = "label2";
            this.lblIdClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(115, 23);
            this.txtClaveSSA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtClaveSSA.MaxLength = 15;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(199, 22);
            this.txtClaveSSA.TabIndex = 0;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_KeyDown);
            this.txtClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Clave SSA :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scTextBoxExt1
            // 
            this.scTextBoxExt1.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.scTextBoxExt1.Decimales = 2;
            this.scTextBoxExt1.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.scTextBoxExt1.ForeColor = System.Drawing.Color.Black;
            this.scTextBoxExt1.Location = new System.Drawing.Point(125, 374);
            this.scTextBoxExt1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scTextBoxExt1.MaxLength = 15;
            this.scTextBoxExt1.Name = "scTextBoxExt1";
            this.scTextBoxExt1.PermitirApostrofo = false;
            this.scTextBoxExt1.PermitirNegativos = false;
            this.scTextBoxExt1.Size = new System.Drawing.Size(199, 22);
            this.scTextBoxExt1.TabIndex = 4;
            this.scTextBoxExt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmAgregarExistenciaClaveDistribuccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 287);
            this.Controls.Add(this.scTextBoxExt1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmAgregarExistenciaClaveDistribuccion";
            this.ShowIcon = false;
            this.Text = "Agregar Existencia a Claves para Distribucción";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReprocesarExistenciaClaveDistribuccion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGenerarExistencia;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scLabelExt lblDescripcion;
        private System.Windows.Forms.Label lblIdClaveSSA;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblExisTransito;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblExistencia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblContPte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPresentacion;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt scTextBoxExt1;
    }
}