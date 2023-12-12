using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales; 
using SC_SolutionsSystem.FP; 

namespace SC_SolutionsSystem.FP.Huellas
{
	/* NOTE: This form is a base for the EnrollmentForm and the VerificationForm,
		All changes in the CaptureForm will be reflected in all its derived forms.
	*/
    public partial class FrmCaptura : Form, DPFP.Capture.EventHandler
	{
        private DPFP.Template Template = new DPFP.Template();
        private DPFP.Capture.Capture Capturer;
        public Size SizeForm;
        public basGenerales Fg;
        private Bitmap imageDefault = new Bitmap(1, 1); 

		public FrmCaptura()
		{
            CheckForIllegalCrossThreadCalls = false;

			InitializeComponent();
            Fg = new basGenerales(this); 

            SizeForm = this.MinimumSize;
            if (!FP_General.EsEquipoDeDesarrollo)
            {
                Picture.Anchor = AnchorStyles.Top & AnchorStyles.Left; 

                int iAlto = 360;
                iAlto = Picture.Height + StatusLine.Height + 28; 

                SizeForm = new Size(Picture.Width + 5, iAlto);
                this.MinimumSize = SizeForm;
                this.Size = SizeForm; 

                CloseButton.Enabled = false;
                CloseButton.Visible = false;


                Picture.Left = -1;
                Picture.Top = -1; 
                Picture.Width += 2;

                //Picture.Left = 0;
                //Picture.Top = 0;
                //Picture.Width += 0; 



                StatusLine.Left = 0; 
                //StatusLine.BorderStyle = BorderStyle.FixedSingle; 
                StatusLine.Top = Picture.Height + 0;
                StatusLine.Width = Picture.Width; 

            }

            Fg.CentrarForma(); 
		}

		protected virtual void Init()
		{
            try
            {
                Capturer = new DPFP.Capture.Capture();				// Create a capture operation.

                if (null != Capturer)
                {
                    Capturer.EventHandler = this;					// Subscribe for capturing events.
                }
                else
                {
                    //SetPrompt("Can't initiate capture operation!");
                    SetPrompt("No es posible iniciar la captura de huella.");
                }
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(string.Format("{0} : {1}", "Init()", ex.Message)); 
                // MessageBox.Show("Can't initiate capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                General.msjError("No es posible iniciar la captura de huella."); 
            }
		}

		protected virtual void Process(DPFP.Sample Sample)
		{
			// Draw fingerprint sample image.
			DrawPicture(ConvertSampleToBitmap(Sample));
		}

		protected void Start()
		{
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture(); 
                    Capturer.StartCapture();
                    //SetPrompt("Using the fingerprint reader, scan your fingerprint.");
                    SetPrompt("Usando el lector de huella, escanear huella digital.");
                }
                catch(Exception ex)
                {
                    //SetPrompt("Can't initiate capture!");
                    clsGrabarError.LogFileError(string.Format("{0} : {1}", "Start()", ex.Message)); 
                    SetPrompt("No es posible iniciar la captura.");
                }
            }
		}

		protected void Stop()
		{
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                    //SetPrompt("Can't terminate capture!");
                    SetPrompt("Captura terminada.");
                }
            }
		}
		
    	#region Form Event Handlers: 
		private void CaptureForm_Load(object sender, EventArgs e)
		{
			Init();
			Start();												// Start capture operation. 
		}

		private void CaptureForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Stop();
		}
        #endregion Form Event Handlers:

        #region EventHandler Members: 
        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
		{
            //MakeReport("The fingerprint sample was captured.");
            MakeReport("La huella de ejemplo fue capturada.");

            //SetPrompt("Scan the same fingerprint again.");
            SetPrompt("Escanee el mismo dedo nuevamente.");
            
            Process(Sample);
		}

		public void OnFingerGone(object Capture, string ReaderSerialNumber)
		{
            //MakeReport("The finger was removed from the fingerprint reader.");
            MakeReport("El dedo fue removido del lector de huella.");
		}

		public void OnFingerTouch(object Capture, string ReaderSerialNumber)
		{
            //MakeReport("The fingerprint reader was touched.");
            MakeReport("El lector de huella ha sido tocado.");
		}

		public void OnReaderConnect(object Capture, string ReaderSerialNumber)
		{
            //MakeReport("The fingerprint reader was connected.");
            MakeReport("El lector de huella esta conectado.");
            FP_General.LectorConectado = true;
            Picture.BackColor = FP_General.FondoLectorConectado;
            this.BackColor = FP_General.FondoLectorConectado; 
		}

		public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
		{
            //MakeReport("The fingerprint reader was disconnected.");
            MakeReport("El lector de huella esta desconectado.");

            DrawPicture(imageDefault); 
            FP_General.LectorConectado = false;
            Picture.BackColor = FP_General.FondoLectorDesconectado;
            this.BackColor = FP_General.FondoLectorDesconectado; 
        }

		public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
		{
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
            {
                //MakeReport("The quality of the fingerprint sample is good.");
                MakeReport("La calidad de la huella es buena.");
            }
            else
            {
                //MakeReport("The quality of the fingerprint sample is poor.");
                MakeReport("La calidad de la huella es mala.");
            }
		}
        #endregion EventHandler Members:

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
		{
			DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();	// Create a sample convertor.
			Bitmap bitmap = null;												            // TODO: the size doesn't matter
			Convertor.ConvertToPicture(Sample, ref bitmap);									// TODO: return bitmap as a result
			return bitmap;
		}

		protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
		{
			DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();	// Create a feature extractor
			DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
			DPFP.FeatureSet features = new DPFP.FeatureSet();

            try
            {
                Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);			// TODO: return features as a result?
            }
            catch { } 

            if (feedback == DPFP.Capture.CaptureFeedback.Good)
            {
                return features;
            }
            else
            {
                return null;
            } 
		}

        public void OnTemplate(DPFP.Template template)
        {
            try 
            {
            this.Invoke(new Function(delegate()
            {
                Template = template;
                if (Template != null)
                {
                    //MessageBox.Show("The fingerprint template is ready for fingerprint verification.", "Fingerprint Enrollment");
                }
                else
                {
                    //MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
                }
            }));
            }
            catch { }
        }

		protected void SetStatus(string status)
		{
            try
            {
                this.Invoke(new Function(delegate()
                {
                    StatusLine.Text = status;
                }));
            }
            catch { } 
		}

		protected void SetPrompt(string prompt)
		{
            try
            {
                this.Invoke(new Function(delegate()
                {
                    Prompt.Text = prompt;
                }));
            }
            catch { }
		}
		protected void MakeReport(string message)
		{
            try 
            {
			this.Invoke(new Function(delegate() {
				StatusText.AppendText(message + "\r\n");
			}));
            }
            catch { }
		}

		private void DrawPicture(Bitmap bitmap)
		{
            try 
            {
			this.Invoke(new Function(delegate() {
				Picture.Image = new Bitmap(bitmap, Picture.Size);	// fit the image into the picture box
			}));
            }
            catch { }
		}
	}
}