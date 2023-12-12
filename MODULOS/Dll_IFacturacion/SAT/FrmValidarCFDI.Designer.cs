namespace Dll_IFacturacion.SAT
{
    partial class FrmValidarCFDI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if(disposing && (components != null))
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
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_013_Folio = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_012_Serie = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReceptor = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRFC_Emisor = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFolioFiscal = new SC_ControlsCS.scTextBoxExt();
            this.txtRespuesta = new System.Windows.Forms.TextBox();
            this.btnLimpiarDatos = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCaptcha = new SC_ControlsCS.scTextBoxExt();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.captchaImg = new System.Windows.Forms.PictureBox();
            this.btnValidar = new System.Windows.Forms.Button();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.FrameResultados = new System.Windows.Forms.GroupBox();
            this.lbl_11_FechaDeCancelacion = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_10_EstatusDeCancelacion = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lbl_09_EstadoDelCFDI = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lbl_08_EfectoDeComprobante = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lbl_07_TotalCFDI = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lbl_06_PAC_Certifico = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_05_FechaCertificacionSAT = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_04_FechaDeExpedicion = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_03_FolioFiscal = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_02_Receptor = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_01_Emisor = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.captchaImg)).BeginInit();
            this.FrameResultados.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(759, 162);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(160, 34);
            this.webBrowser.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_013_Folio);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lbl_012_Serie);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.webBrowser);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtReceptor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtRFC_Emisor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFolioFiscal);
            this.groupBox1.Controls.Add(this.txtRespuesta);
            this.groupBox1.Controls.Add(this.btnLimpiarDatos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCaptcha);
            this.groupBox1.Controls.Add(this.btnImprimir);
            this.groupBox1.Controls.Add(this.captchaImg);
            this.groupBox1.Controls.Add(this.btnValidar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1025, 214);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // lbl_013_Folio
            // 
            this.lbl_013_Folio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_013_Folio.Location = new System.Drawing.Point(190, 177);
            this.lbl_013_Folio.Name = "lbl_013_Folio";
            this.lbl_013_Folio.Size = new System.Drawing.Size(129, 29);
            this.lbl_013_Folio.TabIndex = 19;
            this.lbl_013_Folio.Text = "Folio fiscal";
            this.lbl_013_Folio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(50, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 29);
            this.label11.TabIndex = 18;
            this.label11.Text = "Folio";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_012_Serie
            // 
            this.lbl_012_Serie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_012_Serie.Location = new System.Drawing.Point(190, 145);
            this.lbl_012_Serie.Name = "lbl_012_Serie";
            this.lbl_012_Serie.Size = new System.Drawing.Size(129, 29);
            this.lbl_012_Serie.TabIndex = 17;
            this.lbl_012_Serie.Text = "Folio fiscal";
            this.lbl_012_Serie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(50, 145);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(137, 29);
            this.label15.TabIndex = 16;
            this.label15.Text = "Serie";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(50, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 29);
            this.label4.TabIndex = 15;
            this.label4.Text = "RFC Receptor";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReceptor
            // 
            this.txtReceptor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReceptor.Decimales = 2;
            this.txtReceptor.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReceptor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceptor.ForeColor = System.Drawing.Color.Black;
            this.txtReceptor.Location = new System.Drawing.Point(190, 108);
            this.txtReceptor.MaxLength = 15;
            this.txtReceptor.Name = "txtReceptor";
            this.txtReceptor.PermitirApostrofo = false;
            this.txtReceptor.PermitirNegativos = false;
            this.txtReceptor.Size = new System.Drawing.Size(200, 29);
            this.txtReceptor.TabIndex = 5;
            this.txtReceptor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(50, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 29);
            this.label3.TabIndex = 13;
            this.label3.Text = "RFC Emisor";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRFC_Emisor
            // 
            this.txtRFC_Emisor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRFC_Emisor.Decimales = 2;
            this.txtRFC_Emisor.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRFC_Emisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRFC_Emisor.ForeColor = System.Drawing.Color.Black;
            this.txtRFC_Emisor.Location = new System.Drawing.Point(190, 71);
            this.txtRFC_Emisor.MaxLength = 15;
            this.txtRFC_Emisor.Name = "txtRFC_Emisor";
            this.txtRFC_Emisor.PermitirApostrofo = false;
            this.txtRFC_Emisor.PermitirNegativos = false;
            this.txtRFC_Emisor.Size = new System.Drawing.Size(200, 29);
            this.txtRFC_Emisor.TabIndex = 4;
            this.txtRFC_Emisor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 29);
            this.label2.TabIndex = 11;
            this.label2.Text = "Folio fiscal";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFolioFiscal
            // 
            this.txtFolioFiscal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioFiscal.Decimales = 2;
            this.txtFolioFiscal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolioFiscal.ForeColor = System.Drawing.Color.Black;
            this.txtFolioFiscal.Location = new System.Drawing.Point(190, 34);
            this.txtFolioFiscal.MaxLength = 30;
            this.txtFolioFiscal.Name = "txtFolioFiscal";
            this.txtFolioFiscal.PermitirApostrofo = false;
            this.txtFolioFiscal.PermitirNegativos = false;
            this.txtFolioFiscal.Size = new System.Drawing.Size(460, 29);
            this.txtFolioFiscal.TabIndex = 3;
            this.txtFolioFiscal.Text = "123456";
            this.txtFolioFiscal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRespuesta
            // 
            this.txtRespuesta.Location = new System.Drawing.Point(656, 40);
            this.txtRespuesta.Multiline = true;
            this.txtRespuesta.Name = "txtRespuesta";
            this.txtRespuesta.Size = new System.Drawing.Size(25, 24);
            this.txtRespuesta.TabIndex = 9;
            this.txtRespuesta.Visible = false;
            // 
            // btnLimpiarDatos
            // 
            this.btnLimpiarDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.btnLimpiarDatos.Location = new System.Drawing.Point(759, 35);
            this.btnLimpiarDatos.Name = "btnLimpiarDatos";
            this.btnLimpiarDatos.Size = new System.Drawing.Size(243, 38);
            this.btnLimpiarDatos.TabIndex = 0;
            this.btnLimpiarDatos.Text = "Limpiar datos";
            this.btnLimpiarDatos.UseVisualStyleBackColor = true;
            this.btnLimpiarDatos.Click += new System.EventHandler(this.btnLimpiarDatos_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(473, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 34);
            this.label1.TabIndex = 7;
            this.label1.Text = "Captcha";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // txtCaptcha
            // 
            this.txtCaptcha.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCaptcha.Decimales = 2;
            this.txtCaptcha.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaptcha.ForeColor = System.Drawing.Color.Black;
            this.txtCaptcha.Location = new System.Drawing.Point(473, 158);
            this.txtCaptcha.MaxLength = 6;
            this.txtCaptcha.Name = "txtCaptcha";
            this.txtCaptcha.PermitirApostrofo = false;
            this.txtCaptcha.PermitirNegativos = false;
            this.txtCaptcha.Size = new System.Drawing.Size(148, 38);
            this.txtCaptcha.TabIndex = 6;
            this.txtCaptcha.Text = "123456";
            this.txtCaptcha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCaptcha.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.btnImprimir.Location = new System.Drawing.Point(759, 117);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(243, 38);
            this.btnImprimir.TabIndex = 2;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // captchaImg
            // 
            this.captchaImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.captchaImg.Location = new System.Drawing.Point(479, 84);
            this.captchaImg.Name = "captchaImg";
            this.captchaImg.Size = new System.Drawing.Size(169, 30);
            this.captchaImg.TabIndex = 4;
            this.captchaImg.TabStop = false;
            this.captchaImg.Visible = false;
            // 
            // btnValidar
            // 
            this.btnValidar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.btnValidar.Location = new System.Drawing.Point(759, 76);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(243, 38);
            this.btnValidar.TabIndex = 1;
            this.btnValidar.Text = "Validar";
            this.btnValidar.UseVisualStyleBackColor = true;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // mainTimer
            // 
            this.mainTimer.Enabled = true;
            this.mainTimer.Interval = 1000;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // FrameResultados
            // 
            this.FrameResultados.Controls.Add(this.lbl_11_FechaDeCancelacion);
            this.FrameResultados.Controls.Add(this.label9);
            this.FrameResultados.Controls.Add(this.lbl_10_EstatusDeCancelacion);
            this.FrameResultados.Controls.Add(this.label22);
            this.FrameResultados.Controls.Add(this.lbl_09_EstadoDelCFDI);
            this.FrameResultados.Controls.Add(this.label20);
            this.FrameResultados.Controls.Add(this.lbl_08_EfectoDeComprobante);
            this.FrameResultados.Controls.Add(this.label18);
            this.FrameResultados.Controls.Add(this.lbl_07_TotalCFDI);
            this.FrameResultados.Controls.Add(this.label16);
            this.FrameResultados.Controls.Add(this.lbl_06_PAC_Certifico);
            this.FrameResultados.Controls.Add(this.label14);
            this.FrameResultados.Controls.Add(this.lbl_05_FechaCertificacionSAT);
            this.FrameResultados.Controls.Add(this.label12);
            this.FrameResultados.Controls.Add(this.lbl_04_FechaDeExpedicion);
            this.FrameResultados.Controls.Add(this.label10);
            this.FrameResultados.Controls.Add(this.lbl_03_FolioFiscal);
            this.FrameResultados.Controls.Add(this.label8);
            this.FrameResultados.Controls.Add(this.lbl_02_Receptor);
            this.FrameResultados.Controls.Add(this.label7);
            this.FrameResultados.Controls.Add(this.lbl_01_Emisor);
            this.FrameResultados.Controls.Add(this.label5);
            this.FrameResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrameResultados.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.FrameResultados.Location = new System.Drawing.Point(0, 214);
            this.FrameResultados.Name = "FrameResultados";
            this.FrameResultados.Size = new System.Drawing.Size(1025, 408);
            this.FrameResultados.TabIndex = 1;
            this.FrameResultados.TabStop = false;
            this.FrameResultados.Text = "Resultado";
            // 
            // lbl_11_FechaDeCancelacion
            // 
            this.lbl_11_FechaDeCancelacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_11_FechaDeCancelacion.Location = new System.Drawing.Point(285, 368);
            this.lbl_11_FechaDeCancelacion.Name = "lbl_11_FechaDeCancelacion";
            this.lbl_11_FechaDeCancelacion.Size = new System.Drawing.Size(714, 29);
            this.lbl_11_FechaDeCancelacion.TabIndex = 33;
            this.lbl_11_FechaDeCancelacion.Text = "Folio fiscal";
            this.lbl_11_FechaDeCancelacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(50, 368);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(229, 29);
            this.label9.TabIndex = 32;
            this.label9.Text = "Fecha de cancelación";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_10_EstatusDeCancelacion
            // 
            this.lbl_10_EstatusDeCancelacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_10_EstatusDeCancelacion.Location = new System.Drawing.Point(285, 335);
            this.lbl_10_EstatusDeCancelacion.Name = "lbl_10_EstatusDeCancelacion";
            this.lbl_10_EstatusDeCancelacion.Size = new System.Drawing.Size(714, 29);
            this.lbl_10_EstatusDeCancelacion.TabIndex = 31;
            this.lbl_10_EstatusDeCancelacion.Text = "Folio fiscal";
            this.lbl_10_EstatusDeCancelacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(50, 335);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(229, 29);
            this.label22.TabIndex = 30;
            this.label22.Text = "Estatus de cancelación";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_09_EstadoDelCFDI
            // 
            this.lbl_09_EstadoDelCFDI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_09_EstadoDelCFDI.Location = new System.Drawing.Point(285, 302);
            this.lbl_09_EstadoDelCFDI.Name = "lbl_09_EstadoDelCFDI";
            this.lbl_09_EstadoDelCFDI.Size = new System.Drawing.Size(714, 29);
            this.lbl_09_EstadoDelCFDI.TabIndex = 29;
            this.lbl_09_EstadoDelCFDI.Text = "Folio fiscal";
            this.lbl_09_EstadoDelCFDI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(50, 302);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(229, 29);
            this.label20.TabIndex = 28;
            this.label20.Text = "Estado CFDI";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_08_EfectoDeComprobante
            // 
            this.lbl_08_EfectoDeComprobante.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_08_EfectoDeComprobante.Location = new System.Drawing.Point(285, 269);
            this.lbl_08_EfectoDeComprobante.Name = "lbl_08_EfectoDeComprobante";
            this.lbl_08_EfectoDeComprobante.Size = new System.Drawing.Size(714, 29);
            this.lbl_08_EfectoDeComprobante.TabIndex = 27;
            this.lbl_08_EfectoDeComprobante.Text = "Folio fiscal";
            this.lbl_08_EfectoDeComprobante.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(50, 269);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(229, 29);
            this.label18.TabIndex = 26;
            this.label18.Text = "Efecto del comprobante";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_07_TotalCFDI
            // 
            this.lbl_07_TotalCFDI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_07_TotalCFDI.Location = new System.Drawing.Point(285, 236);
            this.lbl_07_TotalCFDI.Name = "lbl_07_TotalCFDI";
            this.lbl_07_TotalCFDI.Size = new System.Drawing.Size(714, 29);
            this.lbl_07_TotalCFDI.TabIndex = 25;
            this.lbl_07_TotalCFDI.Text = "Folio fiscal";
            this.lbl_07_TotalCFDI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(50, 236);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(229, 29);
            this.label16.TabIndex = 24;
            this.label16.Text = "Total del CFDI";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_06_PAC_Certifico
            // 
            this.lbl_06_PAC_Certifico.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_06_PAC_Certifico.Location = new System.Drawing.Point(285, 203);
            this.lbl_06_PAC_Certifico.Name = "lbl_06_PAC_Certifico";
            this.lbl_06_PAC_Certifico.Size = new System.Drawing.Size(714, 29);
            this.lbl_06_PAC_Certifico.TabIndex = 23;
            this.lbl_06_PAC_Certifico.Text = "Folio fiscal";
            this.lbl_06_PAC_Certifico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(50, 203);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(229, 29);
            this.label14.TabIndex = 22;
            this.label14.Text = "PAC que certificó";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_05_FechaCertificacionSAT
            // 
            this.lbl_05_FechaCertificacionSAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_05_FechaCertificacionSAT.Location = new System.Drawing.Point(285, 170);
            this.lbl_05_FechaCertificacionSAT.Name = "lbl_05_FechaCertificacionSAT";
            this.lbl_05_FechaCertificacionSAT.Size = new System.Drawing.Size(714, 29);
            this.lbl_05_FechaCertificacionSAT.TabIndex = 21;
            this.lbl_05_FechaCertificacionSAT.Text = "Folio fiscal";
            this.lbl_05_FechaCertificacionSAT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(50, 170);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(229, 29);
            this.label12.TabIndex = 20;
            this.label12.Text = "Fecha certificación SAT";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_04_FechaDeExpedicion
            // 
            this.lbl_04_FechaDeExpedicion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_04_FechaDeExpedicion.Location = new System.Drawing.Point(285, 138);
            this.lbl_04_FechaDeExpedicion.Name = "lbl_04_FechaDeExpedicion";
            this.lbl_04_FechaDeExpedicion.Size = new System.Drawing.Size(714, 29);
            this.lbl_04_FechaDeExpedicion.TabIndex = 19;
            this.lbl_04_FechaDeExpedicion.Text = "Folio fiscal";
            this.lbl_04_FechaDeExpedicion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(50, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(229, 29);
            this.label10.TabIndex = 18;
            this.label10.Text = "Fecha de expedición";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_03_FolioFiscal
            // 
            this.lbl_03_FolioFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_03_FolioFiscal.Location = new System.Drawing.Point(190, 105);
            this.lbl_03_FolioFiscal.Name = "lbl_03_FolioFiscal";
            this.lbl_03_FolioFiscal.Size = new System.Drawing.Size(809, 29);
            this.lbl_03_FolioFiscal.TabIndex = 17;
            this.lbl_03_FolioFiscal.Text = "Folio fiscal";
            this.lbl_03_FolioFiscal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(50, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(137, 29);
            this.label8.TabIndex = 16;
            this.label8.Text = "Folio fiscal";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_02_Receptor
            // 
            this.lbl_02_Receptor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_02_Receptor.Location = new System.Drawing.Point(190, 71);
            this.lbl_02_Receptor.Name = "lbl_02_Receptor";
            this.lbl_02_Receptor.Size = new System.Drawing.Size(809, 29);
            this.lbl_02_Receptor.TabIndex = 15;
            this.lbl_02_Receptor.Text = "Folio fiscal";
            this.lbl_02_Receptor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(50, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 29);
            this.label7.TabIndex = 14;
            this.label7.Text = "Receptor";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_01_Emisor
            // 
            this.lbl_01_Emisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbl_01_Emisor.Location = new System.Drawing.Point(190, 39);
            this.lbl_01_Emisor.Name = "lbl_01_Emisor";
            this.lbl_01_Emisor.Size = new System.Drawing.Size(809, 29);
            this.lbl_01_Emisor.TabIndex = 13;
            this.lbl_01_Emisor.Text = "Folio fiscal";
            this.lbl_01_Emisor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(50, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "Emisor";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmValidarCFDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 622);
            this.Controls.Add(this.FrameResultados);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmValidarCFDI";
            this.Text = "Validar CFDI";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.captchaImg)).EndInit();
            this.FrameResultados.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.PictureBox captchaImg;
        private System.Windows.Forms.Button btnValidar;
        private SC_ControlsCS.scTextBoxExt txtCaptcha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLimpiarDatos;
        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.TextBox txtRespuesta;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtReceptor;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtRFC_Emisor;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtFolioFiscal;
        private System.Windows.Forms.GroupBox FrameResultados;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_01_Emisor;
        private System.Windows.Forms.Label lbl_02_Receptor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_03_FolioFiscal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_06_PAC_Certifico;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_05_FechaCertificacionSAT;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_04_FechaDeExpedicion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_07_TotalCFDI;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lbl_09_EstadoDelCFDI;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lbl_08_EfectoDeComprobante;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lbl_10_EstatusDeCancelacion;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lbl_11_FechaDeCancelacion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_013_Folio;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_012_Serie;
        private System.Windows.Forms.Label label15;
    }
}