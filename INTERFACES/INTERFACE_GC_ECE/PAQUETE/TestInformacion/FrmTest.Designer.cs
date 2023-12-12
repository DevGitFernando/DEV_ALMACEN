namespace TestInformacion
{
    partial class FrmTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTest));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtClaveSSA = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIdFarmacia = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdEstado = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoExistenciaClaveGrupos = new System.Windows.Forms.RadioButton();
            this.rdoExistenciaClave = new System.Windows.Forms.RadioButton();
            this.rdoCuadroBasico = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lstvwResultados = new System.Windows.Forms.ListView();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtClaveSSA);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtIdFarmacia);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtIdEstado);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 51);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parametros";
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.Location = new System.Drawing.Point(337, 19);
            this.txtClaveSSA.MaxLength = 20;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.Size = new System.Drawing.Size(113, 20);
            this.txtClaveSSA.TabIndex = 5;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(268, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Clave SSA : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdFarmacia
            // 
            this.txtIdFarmacia.Enabled = false;
            this.txtIdFarmacia.Location = new System.Drawing.Point(194, 19);
            this.txtIdFarmacia.MaxLength = 4;
            this.txtIdFarmacia.Name = "txtIdFarmacia";
            this.txtIdFarmacia.Size = new System.Drawing.Size(63, 20);
            this.txtIdFarmacia.TabIndex = 3;
            this.txtIdFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(137, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Farmacia : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdEstado
            // 
            this.txtIdEstado.Enabled = false;
            this.txtIdEstado.Location = new System.Drawing.Point(68, 19);
            this.txtIdEstado.MaxLength = 2;
            this.txtIdEstado.Name = "txtIdEstado";
            this.txtIdEstado.Size = new System.Drawing.Size(63, 20);
            this.txtIdEstado.TabIndex = 1;
            this.txtIdEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estado : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoExistenciaClaveGrupos);
            this.groupBox3.Controls.Add(this.rdoExistenciaClave);
            this.groupBox3.Controls.Add(this.rdoCuadroBasico);
            this.groupBox3.Location = new System.Drawing.Point(482, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(435, 51);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Consulta";
            // 
            // rdoExistenciaClaveGrupos
            // 
            this.rdoExistenciaClaveGrupos.Location = new System.Drawing.Point(262, 21);
            this.rdoExistenciaClaveGrupos.Name = "rdoExistenciaClaveGrupos";
            this.rdoExistenciaClaveGrupos.Size = new System.Drawing.Size(168, 18);
            this.rdoExistenciaClaveGrupos.TabIndex = 2;
            this.rdoExistenciaClaveGrupos.Text = "Existencia por clave y grupo";
            this.rdoExistenciaClaveGrupos.UseVisualStyleBackColor = true;
            // 
            // rdoExistenciaClave
            // 
            this.rdoExistenciaClave.Location = new System.Drawing.Point(129, 21);
            this.rdoExistenciaClave.Name = "rdoExistenciaClave";
            this.rdoExistenciaClave.Size = new System.Drawing.Size(122, 18);
            this.rdoExistenciaClave.TabIndex = 1;
            this.rdoExistenciaClave.Text = "Existencia por clave";
            this.rdoExistenciaClave.UseVisualStyleBackColor = true;
            // 
            // rdoCuadroBasico
            // 
            this.rdoCuadroBasico.Checked = true;
            this.rdoCuadroBasico.Location = new System.Drawing.Point(19, 21);
            this.rdoCuadroBasico.Name = "rdoCuadroBasico";
            this.rdoCuadroBasico.Size = new System.Drawing.Size(94, 18);
            this.rdoCuadroBasico.TabIndex = 0;
            this.rdoCuadroBasico.TabStop = true;
            this.rdoCuadroBasico.Text = "Cuadro básico";
            this.rdoCuadroBasico.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtUrl);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.lstvwResultados);
            this.groupBox4.Location = new System.Drawing.Point(12, 63);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(905, 378);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Resultado";
            // 
            // lstvwResultados
            // 
            this.lstvwResultados.FullRowSelect = true;
            this.lstvwResultados.Location = new System.Drawing.Point(16, 19);
            this.lstvwResultados.Name = "lstvwResultados";
            this.lstvwResultados.Size = new System.Drawing.Size(871, 321);
            this.lstvwResultados.TabIndex = 2;
            this.lstvwResultados.UseCompatibleStateImageBehavior = false;
            this.lstvwResultados.View = System.Windows.Forms.View.Details;
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Location = new System.Drawing.Point(768, 447);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(149, 23);
            this.btnEjecutar.TabIndex = 4;
            this.btnEjecutar.Text = "Consultar";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(613, 447);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(149, 23);
            this.btnLimpiar.TabIndex = 5;
            this.btnLimpiar.Text = "Limpiar pantalla";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(45, 346);
            this.txtUrl.MaxLength = 50;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(328, 20);
            this.txtUrl.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Url : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 478);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test conexión SII Farmacia";
            this.Load += new System.EventHandler(this.FrmTest_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoCuadroBasico;
        private System.Windows.Forms.RadioButton rdoExistenciaClave;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClaveSSA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIdFarmacia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIdEstado;
        private System.Windows.Forms.RadioButton rdoExistenciaClaveGrupos;
        private System.Windows.Forms.ListView lstvwResultados;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label4;
    }
}