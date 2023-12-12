using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FP;

namespace SC_SolutionsSystem.FP.Huellas
{
	/* NOTE: This form is inherited from the CaptureForm,
		so the VisualStudio Form Designer may not load it properly
		(at least until you build the project).
		If you want to make changes in the form layout - do it in the base CaptureForm.
		All changes in the CaptureForm will be reflected in all derived forms 
		(i.e. in the EnrollmentForm and in the VerificationForm)
	*/
    public class LeerHuella : FrmCaptura
	{
		public delegate void OnTemplateEventHandler(DPFP.Template template);

		// public event OnTemplateEventHandler OnTemplate; 
        private DPFP.Processing.Enrollment Enroller = new DPFP.Processing.Enrollment();
        DPFP.Template template;
        DPFP.FeatureSet verFeatures;

        byte[] huella = null; 
        clsConexionSQL cnn = new clsConexionSQL(FP_General.Conexion);
        clsLeer leerHuella;
        clsLeer guardarHuella;
        public string Titulo = "Lectura de huella digital"; 

		protected override void Init()
		{
            //CheckForIllegalCrossThreadCalls = false;

			base.Init();
			base.Text = "Fingerprint Enrollment";
            base.Text = "Lectura de huella digital";
            base.Text = Titulo; 

            ////this.MinimumSize = base.SizeForm;
            //this.Size = base.SizeForm; 

            FP_General.HuellaRegistrada = false; 
            FP_General.HuellaCapturada = false; 
			Enroller = new DPFP.Processing.Enrollment();			// Create an enrollment. 

			UpdateStatus();
            
            leerHuella = new clsLeer(ref cnn);
            guardarHuella = new clsLeer(ref cnn); 
		}

		protected override void Process(DPFP.Sample Sample)
		{
			base.Process(Sample);

			// Process the sample and create a feature set for the enrollment purpose.
			DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

			// Check quality of the sample and add to enroller if it's good
            if (features != null)
            {
                try
                {
                    //MakeReport("The fingerprint feature set was created."); 
                    MakeReport("El conjunto de características de huellas dactilares fue creado.");
                    Enroller.AddFeatures(features);		// Add feature set to template.
                }
                finally
                {
                    UpdateStatus();

                    // Check if template has been created.
                    switch (Enroller.TemplateStatus)
                    {
                        case DPFP.Processing.Enrollment.Status.Ready:	// report success and stop capturing 

                            OnTemplate(Enroller.Template);
                            // SetPrompt("Click Close, and then click Fingerprint Verification.");
                            SetPrompt("De click en Cerrar, y verifique la huella leida.");
                            Stop();

                            MemoryStream x = new MemoryStream();
                            MemoryStream mem = new MemoryStream();
                            template = Enroller.Template;
                            template.Serialize(mem);

                            verFeatures = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);
                            FP_General.CompararHuella(verFeatures, true);


                            if (!FP_General.ExisteHuella)
                            {
                                huella = mem.GetBuffer();

                                if (FP_General.RegistrarHuella(huella, FP_General.Dedo))
                                {
                                    // General.msjAviso("Huella registrada satisfactoriamente.");
                                }
                            }
                            this.Close(); 
                            break;

                        case DPFP.Processing.Enrollment.Status.Failed:	// report failure and restart capturing
                            Enroller.Clear();
                            Stop();
                            UpdateStatus();
                            OnTemplate(null);
                            Start();
                            break;
                    }
                }
            }
		}

		private void UpdateStatus()
		{
			// Show number of samples needed.
			// SetStatus(String.Format("Fingerprint samples needed: {0}", Enroller.FeaturesNeeded));
            SetStatus(String.Format("Lectura de huella requeridas : {0}", Enroller.FeaturesNeeded)); 
		}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LeerHuella
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(621, 354);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "LeerHuella";
            this.Load += new System.EventHandler(this.LeerHuella_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LeerHuella_Load(object sender, EventArgs e)
        {

        }
    }
}
