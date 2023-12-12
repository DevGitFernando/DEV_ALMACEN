using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices; 
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO; 

using AForge.Video;
using AForge.Imaging;
using AForge.Video.DirectShow;

using ZXing;
using SC_SolutionsSystem.Errores; 
using SC_SolutionsSystem.QRCode.Codec; 

namespace SC_SolutionsSystem.QRCode
{
    public class Cam_Reader
    {
        #region Declaracion de Variables 
        // QRCamControl camControl;
        // QRProcess process;

        FrmCamDigitalizar f; 

        string sCamaraProceso = "";
        string sResultado = "";
        bool bDatosLeidos = false;
        public Result Resultado;
        Bitmap imgCamara = null;
        bool bImagenDigitalizada = false;
        string sImg_Original = "";
        string sImg_Comprimida = "";

        string sFileName = "Digitalizacion";
        string sFileNameCompress = "DigitalizacionCompress";

        BinaryReader file;

        #endregion Declaracion de Variables 

        #region Constructores y Destructores de Clase  
        public Cam_Reader()
        {
            ////camControl = new QRCamControl(); 
            ////process = new QRProcess();
        }

        ~Cam_Reader()
        {
            f = null; 
        }
        #endregion Constructores y Destructores de Clase 

        #region Propiedades Publicas 
        public string Camara
        {
            get { return sCamaraProceso; }
            set { sCamaraProceso = value; }
        }

        public bool ImagenDigitalizada
        {
            get { return bImagenDigitalizada; }
        }

        public Bitmap Imagen
        {
            get { return imgCamara; }
        }

        public string FileName
        {
            get { return sFileName; }
        }

        public string FileNameCompress
        {
            get { return sFileNameCompress; }
        }
        #endregion Propiedades Publicas 

        #region Funciones y Procedimientos Publicos 
        public void Show()
        {
            bDatosLeidos = false;
            sResultado = "";
            sImg_Original = "";
            sImg_Comprimida = ""; 


            try
            {
                f = new FrmCamDigitalizar(sCamaraProceso);
                f.ShowReader();

                bImagenDigitalizada = f.ImagenDigitalizada;
                sFileName = f.FileName;
                sFileNameCompress = f.FileNameCompress; 

                if (bImagenDigitalizada)
                {
                    using (System.Drawing.Image img = System.Drawing.Image.FromFile(sFileName))
                    {
                        imgCamara = (Bitmap)img;
                    }
                    //imgCamara = f.Imagen;


                    if (imgCamara == null)
                    {
                        General.msjError("Error al capturar la imagen.");
                    }
                }
                
            }
            catch (Exception ex)   
            {
                ex.Source = ex.Source;
                clsGrabarError.LogFileError(string.Format("Función : {0}, Mensaje {1} ", "Cam_Reader.Show()", ex.Message));
            }

            //////camControl = new QRCamControl();
            //////process = new QRProcess();
            //////process.ResultFound += new EventHandler<ProcessArgs>(process_ResultFound);
            //////process.Start();

            //////camControl.SetCamera(sCamaraProceso);
            //////camControl.SelectedDevice.NewFrame += new NewFrameEventHandler(captureDevice_NewFrame); 
            //////camControl.SelectedDevice.Start();

        } 
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        #endregion Funciones y Procedimientos Privados 
    } 
}
