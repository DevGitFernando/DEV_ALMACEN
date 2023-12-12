namespace Farmacia.CierreOperacion
{
    partial class FrmCierreDeInventario
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
            this.FrameAviso = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.FrameCerrarInventario = new System.Windows.Forms.GroupBox();
            this.btnGenerarRespaldo = new System.Windows.Forms.Button();
            this.btnGenerarCierre = new System.Windows.Forms.Button();
            this.FrameAvance = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.lblAvance = new SC_ControlsCS.scLabelExt();
            this.tmAvance = new System.Windows.Forms.Timer(this.components);
            this.tmProceso = new System.Windows.Forms.Timer(this.components);
            this.FrameUnidad = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFarmaciaNueva = new SC_ControlsCS.scLabelExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFarmacia = new SC_ControlsCS.scLabelExt();
            this.FrameReportes = new System.Windows.Forms.GroupBox();
            this.btnExportarReportes = new System.Windows.Forms.Button();
            this.lblDirectorioExportar = new SC_ControlsCS.scLabelExt();
            this.tmPantalla = new System.Windows.Forms.Timer(this.components);
            this.FrameTipoDeProceso = new System.Windows.Forms.GroupBox();
            this.lblTipoDeProceso = new System.Windows.Forms.Label();
            this.FrameAviso.SuspendLayout();
            this.FrameCerrarInventario.SuspendLayout();
            this.FrameAvance.SuspendLayout();
            this.FrameUnidad.SuspendLayout();
            this.FrameReportes.SuspendLayout();
            this.FrameTipoDeProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameAviso
            // 
            this.FrameAviso.Controls.Add(this.label2);
            this.FrameAviso.Controls.Add(this.label1);
            this.FrameAviso.Controls.Add(this.lblMensaje);
            this.FrameAviso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrameAviso.Location = new System.Drawing.Point(10, 6);
            this.FrameAviso.Name = "FrameAviso";
            this.FrameAviso.Size = new System.Drawing.Size(454, 175);
            this.FrameAviso.TabIndex = 22;
            this.FrameAviso.TabStop = false;
            this.FrameAviso.Text = "Importante";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(16, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(424, 49);
            this.label2.TabIndex = 22;
            this.label2.Text = "3.- Se recomienda generar un respaldo de la Base de Datos antes y después de ejec" +
    "utar este proceso.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(424, 42);
            this.label1.TabIndex = 21;
            this.label1.Text = "1.- Este proceso sólo se puede ejecutar en el Servidor de la Unidad";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMensaje
            // 
            this.lblMensaje.BackColor = System.Drawing.Color.Transparent;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMensaje.Location = new System.Drawing.Point(16, 65);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(424, 42);
            this.lblMensaje.TabIndex = 20;
            this.lblMensaje.Text = "2.- Se generará el cierre de inventario, no es posible deshacer esta operación.";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameCerrarInventario
            // 
            this.FrameCerrarInventario.Controls.Add(this.btnGenerarRespaldo);
            this.FrameCerrarInventario.Controls.Add(this.btnGenerarCierre);
            this.FrameCerrarInventario.Location = new System.Drawing.Point(10, 407);
            this.FrameCerrarInventario.Name = "FrameCerrarInventario";
            this.FrameCerrarInventario.Size = new System.Drawing.Size(454, 86);
            this.FrameCerrarInventario.TabIndex = 23;
            this.FrameCerrarInventario.TabStop = false;
            // 
            // btnGenerarRespaldo
            // 
            this.btnGenerarRespaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarRespaldo.Location = new System.Drawing.Point(229, 18);
            this.btnGenerarRespaldo.Name = "btnGenerarRespaldo";
            this.btnGenerarRespaldo.Size = new System.Drawing.Size(211, 54);
            this.btnGenerarRespaldo.TabIndex = 1;
            this.btnGenerarRespaldo.Text = "Generar respaldo";
            this.btnGenerarRespaldo.UseVisualStyleBackColor = true;
            this.btnGenerarRespaldo.Click += new System.EventHandler(this.btnGenerarRespaldo_Click);
            // 
            // btnGenerarCierre
            // 
            this.btnGenerarCierre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarCierre.Location = new System.Drawing.Point(14, 18);
            this.btnGenerarCierre.Name = "btnGenerarCierre";
            this.btnGenerarCierre.Size = new System.Drawing.Size(211, 54);
            this.btnGenerarCierre.TabIndex = 0;
            this.btnGenerarCierre.Text = "Generar cierre";
            this.btnGenerarCierre.UseVisualStyleBackColor = true;
            this.btnGenerarCierre.Click += new System.EventHandler(this.btnGenerarCierre_Click);
            // 
            // FrameAvance
            // 
            this.FrameAvance.Controls.Add(this.pgBar);
            this.FrameAvance.Controls.Add(this.lblAvance);
            this.FrameAvance.Location = new System.Drawing.Point(10, 497);
            this.FrameAvance.Name = "FrameAvance";
            this.FrameAvance.Size = new System.Drawing.Size(454, 86);
            this.FrameAvance.TabIndex = 24;
            this.FrameAvance.TabStop = false;
            this.FrameAvance.Visible = false;
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(14, 16);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(426, 38);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 25;
            // 
            // lblAvance
            // 
            this.lblAvance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAvance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvance.Location = new System.Drawing.Point(14, 57);
            this.lblAvance.MostrarToolTip = false;
            this.lblAvance.Name = "lblAvance";
            this.lblAvance.Size = new System.Drawing.Size(426, 21);
            this.lblAvance.TabIndex = 1;
            this.lblAvance.Text = "scLabelExt1";
            this.lblAvance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmAvance
            // 
            this.tmAvance.Interval = 5000;
            this.tmAvance.Tick += new System.EventHandler(this.tmAvance_Tick);
            // 
            // tmProceso
            // 
            this.tmProceso.Tick += new System.EventHandler(this.tmProceso_Tick);
            // 
            // FrameUnidad
            // 
            this.FrameUnidad.Controls.Add(this.label4);
            this.FrameUnidad.Controls.Add(this.lblFarmaciaNueva);
            this.FrameUnidad.Controls.Add(this.label3);
            this.FrameUnidad.Controls.Add(this.lblFarmacia);
            this.FrameUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrameUnidad.Location = new System.Drawing.Point(10, 260);
            this.FrameUnidad.Name = "FrameUnidad";
            this.FrameUnidad.Size = new System.Drawing.Size(454, 86);
            this.FrameUnidad.TabIndex = 24;
            this.FrameUnidad.TabStop = false;
            this.FrameUnidad.Text = "Información de Farmacia";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(17, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nueva :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFarmaciaNueva
            // 
            this.lblFarmaciaNueva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmaciaNueva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFarmaciaNueva.Location = new System.Drawing.Point(83, 51);
            this.lblFarmaciaNueva.MostrarToolTip = false;
            this.lblFarmaciaNueva.Name = "lblFarmaciaNueva";
            this.lblFarmaciaNueva.Size = new System.Drawing.Size(357, 23);
            this.lblFarmaciaNueva.TabIndex = 2;
            this.lblFarmaciaNueva.Text = "scLabelExt2";
            this.lblFarmaciaNueva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(17, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Actual :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFarmacia.Location = new System.Drawing.Point(83, 24);
            this.lblFarmacia.MostrarToolTip = false;
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(357, 23);
            this.lblFarmacia.TabIndex = 0;
            this.lblFarmacia.Text = "scLabelExt1";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameReportes
            // 
            this.FrameReportes.Controls.Add(this.btnExportarReportes);
            this.FrameReportes.Controls.Add(this.lblDirectorioExportar);
            this.FrameReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrameReportes.Location = new System.Drawing.Point(10, 348);
            this.FrameReportes.Name = "FrameReportes";
            this.FrameReportes.Size = new System.Drawing.Size(454, 57);
            this.FrameReportes.TabIndex = 25;
            this.FrameReportes.TabStop = false;
            this.FrameReportes.Text = "Directorio de reportes";
            // 
            // btnExportarReportes
            // 
            this.btnExportarReportes.Location = new System.Drawing.Point(415, 24);
            this.btnExportarReportes.Name = "btnExportarReportes";
            this.btnExportarReportes.Size = new System.Drawing.Size(25, 23);
            this.btnExportarReportes.TabIndex = 1;
            this.btnExportarReportes.Text = "...";
            this.btnExportarReportes.UseVisualStyleBackColor = true;
            this.btnExportarReportes.Click += new System.EventHandler(this.btnExportarReportes_Click);
            // 
            // lblDirectorioExportar
            // 
            this.lblDirectorioExportar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDirectorioExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDirectorioExportar.Location = new System.Drawing.Point(14, 24);
            this.lblDirectorioExportar.MostrarToolTip = false;
            this.lblDirectorioExportar.Name = "lblDirectorioExportar";
            this.lblDirectorioExportar.Size = new System.Drawing.Size(398, 23);
            this.lblDirectorioExportar.TabIndex = 0;
            this.lblDirectorioExportar.Text = "scLabelExt1";
            this.lblDirectorioExportar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tmPantalla
            // 
            this.tmPantalla.Tick += new System.EventHandler(this.tmPantalla_Tick);
            // 
            // FrameTipoDeProceso
            // 
            this.FrameTipoDeProceso.Controls.Add(this.lblTipoDeProceso);
            this.FrameTipoDeProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrameTipoDeProceso.Location = new System.Drawing.Point(10, 183);
            this.FrameTipoDeProceso.Name = "FrameTipoDeProceso";
            this.FrameTipoDeProceso.Size = new System.Drawing.Size(454, 75);
            this.FrameTipoDeProceso.TabIndex = 26;
            this.FrameTipoDeProceso.TabStop = false;
            this.FrameTipoDeProceso.Text = "Tipo de proceso";
            // 
            // lblTipoDeProceso
            // 
            this.lblTipoDeProceso.BackColor = System.Drawing.Color.Transparent;
            this.lblTipoDeProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoDeProceso.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTipoDeProceso.Location = new System.Drawing.Point(15, 22);
            this.lblTipoDeProceso.Name = "lblTipoDeProceso";
            this.lblTipoDeProceso.Size = new System.Drawing.Size(424, 42);
            this.lblTipoDeProceso.TabIndex = 21;
            this.lblTipoDeProceso.Text = "2.- Se generará el cierre de inventario, no es posible deshacer esta operación.";
            this.lblTipoDeProceso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmCierreDeInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 592);
            this.Controls.Add(this.FrameTipoDeProceso);
            this.Controls.Add(this.FrameReportes);
            this.Controls.Add(this.FrameUnidad);
            this.Controls.Add(this.FrameAvance);
            this.Controls.Add(this.FrameCerrarInventario);
            this.Controls.Add(this.FrameAviso);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmCierreDeInventario";
            this.Text = "Cierre de Inventario";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCierreDeInventario_Load);
            this.FrameAviso.ResumeLayout(false);
            this.FrameCerrarInventario.ResumeLayout(false);
            this.FrameAvance.ResumeLayout(false);
            this.FrameUnidad.ResumeLayout(false);
            this.FrameReportes.ResumeLayout(false);
            this.FrameTipoDeProceso.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameAviso;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.GroupBox FrameCerrarInventario;
        private System.Windows.Forms.Button btnGenerarCierre;
        private System.Windows.Forms.GroupBox FrameAvance;
        private SC_ControlsCS.scLabelExt lblAvance;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Timer tmAvance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenerarRespaldo;
        private System.Windows.Forms.Timer tmProceso;
        private System.Windows.Forms.GroupBox FrameUnidad;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scLabelExt lblFarmaciaNueva;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scLabelExt lblFarmacia;
        private System.Windows.Forms.GroupBox FrameReportes;
        private SC_ControlsCS.scLabelExt lblDirectorioExportar;
        private System.Windows.Forms.Button btnExportarReportes;
        private System.Windows.Forms.Timer tmPantalla;
        private System.Windows.Forms.GroupBox FrameTipoDeProceso;
        private System.Windows.Forms.Label lblTipoDeProceso;
    }
}