using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using AForge.Video;
using AForge.Imaging;
using AForge.Video.DirectShow;

using ZXing; 

namespace ZXing
{
    public class ReaderCam
    {
        ReaderCamControl camControl;
        ReaderProcess process;
        public Result Resultado;

        string sCamaraProceso = "";
        string sResultado = "";
        bool bDatosLeidos = false;

        Bitmap imgCamara = null;
        PictureBox picImagen = new PictureBox();
        private BarcodeFormat formatoDeCodigo = BarcodeFormat.NONE; 

        #region Constructores 
        public ReaderCam(string Camara, BarcodeFormat Formato):this(Camara, Formato, new Size(320, 240))
        { 
            ; 
        }

        public ReaderCam(string Camara, BarcodeFormat Formato, Size FrameSize)
        {
            sCamaraProceso = Camara;
            formatoDeCodigo = Formato; 

            process = new ReaderProcess(Formato);
            process.ResultFound += new EventHandler<ProcessArgs>(process_ResultFound);
            //process.Start();


            camControl = new ReaderCamControl();
            camControl.SetCamera(sCamaraProceso);
            try
            {
                if (camControl.SelectedDevice != null)
                {
                    camControl.SelectedDevice.NewFrame += new NewFrameEventHandler(captureDevice_NewFrame);
                    camControl.SelectedDevice.NewFrame += new NewFrameEventHandler(video_NuevoFrame);
                    camControl.SelectedDevice.DesiredFrameSize = FrameSize;
                }
            }
            catch 
            { 
            }

            ////camControl.SelectedDevice.DesiredFrameSize = new Size(320, 240);
            ////camControl.SelectedDevice.Start();
        }

        ~ReaderCam()
        {
            DetenerCaptura();
            process.Run = false; 
            process = null; 
            camControl = null;
        }
        #endregion Constructores

        #region Propiedades Publicas
        public string Camara
        {
            get { return sCamaraProceso; }
            // set { sCamaraProceso = value; }
        }

        public string DatosDecodificados
        {
            get { return sResultado; }
        }

        public bool DatosLeidos
        {
            get { return bDatosLeidos; }
        }
        #endregion Propiedades Publicas  

        #region Funciones y Procedimientos Publicos 
        public void IniciarCaptura()
        {
            try
            {
                if (camControl.SelectedDevice != null)
                {
                    process.Start();
                    if (!camControl.SelectedDevice.IsRunning)
                    {
                        camControl.SelectedDevice.Start();
                    }
                }
            }
            catch { }
        }

        public void DetenerCaptura()
        {
            try
            {
                if (camControl.SelectedDevice != null)
                {
                    if (camControl.SelectedDevice.IsRunning)
                    {
                        camControl.SelectedDevice.SignalToStop();
                    }
                    process.Stop();
                }
            }
            catch 
            { 
            }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Eventos
        public event EventHandler<ProcessArgs> OnLectura;


        void process_ResultFound(object sender, ProcessArgs e)
        {
            Resultado = e.Resultado;
            sResultado = e.Resultado.Text;
            bDatosLeidos = true;

            ProcessArgs args = new ProcessArgs() { Resultado = e.Resultado }; 
            this.OnLectura(this, args);

            ////DetenerCamara();
        }

        private void video_NuevoFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            picImagen.Image = Imagen;
        }

        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(IntPtr objectHandle);

        void captureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            using (UnmanagedImage uimage = UnmanagedImage.FromManagedImage(eventArgs.Frame))
            {
                try
                {
                    using (Bitmap image = uimage.ToManagedImage())
                    {
                        IntPtr hBitMap = image.GetHbitmap();
                        try
                        {
                            imgCamara = image;
                            process.NewBitmap(image);
                        }
                        catch (Exception ex)
                        {
                            ex.Source = ex.Source;
                        }
                        finally
                        {
                            DeleteObject(hBitMap);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }
        #endregion Eventos   
    }
}
