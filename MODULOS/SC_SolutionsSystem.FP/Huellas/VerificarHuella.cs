using System;
using System.Collections.Generic;
using System.Text;

namespace SC_SolutionsSystem.FP.Huellas
{
	/* NOTE: This form is inherited from the CaptureForm,
		so the VisualStudio Form Designer may not load it properly
		(at least until you build the project).
		If you want to make changes in the form layout - do it in the base CaptureForm.
		All changes in the CaptureForm will be reflected in all derived forms 
		(i.e. in the EnrollmentForm and in the VerificationForm)
	*/
    public class VerificarHuella : FrmCaptura
	{
        private DPFP.Template Template = new DPFP.Template();
        private DPFP.Verification.Verification Verificator; 
        DPFP.FeatureSet verFeatures;
        private int iIntentosVerificacion = FP_General.IntentosVerificacion;

        public string Titulo = "Verificación de huella digital"; 

		public void Verify(DPFP.Template template)
		{
			Template = template;
			ShowDialog();
		}

		protected override void Init()
		{
            //CheckForIllegalCrossThreadCalls = false;

			base.Init();
			base.Text = "Fingerprint Verification";
            base.Text = "Verificación de huella digital";
            base.Text = Titulo; 

            ////this.MinimumSize = base.SizeForm;
            //this.Size = base.SizeForm; 

            FP_General.HuellaRegistrada = false;
            FP_General.HuellaCapturada = false; 

            Verificator = new DPFP.Verification.Verification();		// Create a fingerprint template verificator 
            Template = new DPFP.Template(); 

			UpdateStatus(0);
		}

		protected override void Process(DPFP.Sample Sample)
		{
			base.Process(Sample);

			// Process the sample and create a feature set for the enrollment purpose.
			DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);
            int iFARAchieved = 0; 

			// Check quality of the sample and start verification if it's good
			// TODO: move to a separate task
			if (features != null)
			{
                //////// Compare the feature set with our template
                //////DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                //////Verificator.Verify(features, Template, ref result);
                //////UpdateStatus(result.FARAchieved);

                //////if (!result.Verified)
                //////{
                //////    // MakeReport("The fingerprint was NOT VERIFIED.");
                //////    MakeReport("Huella digital no encontrada."); 
                //////}
                //////else 
                {
                    verFeatures = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);
                    iFARAchieved = FP_General.CompararHuella(verFeatures);
                    

                    iIntentosVerificacion--;
                    UpdateStatus(iFARAchieved);


                    if (!FP_General.ExisteHuella)
                    {
                        MakeReport("Huella digital no encontrada.".ToUpper());
                        if (iIntentosVerificacion <= 0)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MakeReport("Huella digital encontrada.".ToUpper());
                        this.Close(); 
                    }
                }

                ////if (result.Verified)
                ////{
                ////    MakeReport("The fingerprint was VERIFIED.");
                ////}
                ////else
                ////{
                ////    MakeReport("The fingerprint was NOT VERIFIED.");
                ////}
			}
		}

		private void UpdateStatus(int FAR)
		{
            string sMsj = ""; 
            ////// Show "False accept rate" value
            ////SetStatus(String.Format("False Accept Rate (FAR) = {0}", FAR)); 
            ////SetStatus(String.Format("Lectura de huella requeridas : {0}", iIntentosVerificacion));

            sMsj = String.Format("False Accept Rate (FAR) = {0} \r\n", FAR); 
            sMsj += String.Format("Lectura de huella requeridas : {0}", iIntentosVerificacion); 

            if (!FP_General.EsEquipoDeDesarrollo)
            {
                sMsj = String.Format("Lectura de huella requeridas : {0}", iIntentosVerificacion); 
            }

            SetStatus(sMsj); 
		} 
	}
}