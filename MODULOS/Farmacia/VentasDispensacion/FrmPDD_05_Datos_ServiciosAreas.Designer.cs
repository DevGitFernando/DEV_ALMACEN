namespace Farmacia.VentasDispensacion
{
    partial class FrmPDD_05_Datos_ServiciosAreas
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
            this.cboAreas = new SC_ControlsCS.scComboBoxExt();
            this.label15 = new System.Windows.Forms.Label();
            this.cboServicios = new SC_ControlsCS.scComboBoxExt();
            this.label14 = new System.Windows.Forms.Label();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.lblCerrar = new System.Windows.Forms.Label();
            this.FrameDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboAreas
            // 
            this.cboAreas.BackColorEnabled = System.Drawing.Color.White;
            this.cboAreas.Data = "";
            this.cboAreas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAreas.Filtro = " 1 = 1";
            this.cboAreas.FormattingEnabled = true;
            this.cboAreas.ListaItemsBusqueda = 20;
            this.cboAreas.Location = new System.Drawing.Point(80, 50);
            this.cboAreas.MostrarToolTip = false;
            this.cboAreas.Name = "cboAreas";
            this.cboAreas.Size = new System.Drawing.Size(412, 21);
            this.cboAreas.TabIndex = 25;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(15, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 14);
            this.label15.TabIndex = 27;
            this.label15.Text = "Area :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboServicios
            // 
            this.cboServicios.BackColorEnabled = System.Drawing.Color.White;
            this.cboServicios.Data = "";
            this.cboServicios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServicios.Filtro = " 1 = 1";
            this.cboServicios.FormattingEnabled = true;
            this.cboServicios.ListaItemsBusqueda = 20;
            this.cboServicios.Location = new System.Drawing.Point(80, 21);
            this.cboServicios.MostrarToolTip = false;
            this.cboServicios.Name = "cboServicios";
            this.cboServicios.Size = new System.Drawing.Size(412, 21);
            this.cboServicios.TabIndex = 24;
            this.cboServicios.SelectedIndexChanged += new System.EventHandler(this.cboServicios_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(15, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 15);
            this.label14.TabIndex = 26;
            this.label14.Text = "Servicio :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.cboAreas);
            this.FrameDatos.Controls.Add(this.label14);
            this.FrameDatos.Controls.Add(this.label15);
            this.FrameDatos.Controls.Add(this.cboServicios);
            this.FrameDatos.Location = new System.Drawing.Point(10, 8);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(506, 88);
            this.FrameDatos.TabIndex = 28;
            this.FrameDatos.TabStop = false;
            // 
            // lblCerrar
            // 
            this.lblCerrar.BackColor = System.Drawing.Color.Black;
            this.lblCerrar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCerrar.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCerrar.Location = new System.Drawing.Point(0, 102);
            this.lblCerrar.Name = "lblCerrar";
            this.lblCerrar.Size = new System.Drawing.Size(525, 24);
            this.lblCerrar.TabIndex = 29;
            this.lblCerrar.Text = "<F12> Cerrar pantalla        [ X ]  ";
            this.lblCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCerrar.Click += new System.EventHandler(this.lblCerrar_Click);
            // 
            // FrmPDD_05_Datos_ServiciosAreas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 126);
            this.ControlBox = false;
            this.Controls.Add(this.lblCerrar);
            this.Controls.Add(this.FrameDatos);
            this.Name = "FrmPDD_05_Datos_ServiciosAreas";
            this.Text = "Información de servicio y área";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPDD_05_Datos_ServiciosAreas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPDD_05_Datos_ServiciosAreas_KeyDown);
            this.FrameDatos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SC_ControlsCS.scComboBoxExt cboAreas;
        private System.Windows.Forms.Label label15;
        private SC_ControlsCS.scComboBoxExt cboServicios;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox FrameDatos;
        private System.Windows.Forms.Label lblCerrar;
    }
}