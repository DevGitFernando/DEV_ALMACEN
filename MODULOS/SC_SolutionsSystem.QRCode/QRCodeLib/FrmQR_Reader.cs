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
using SC_SolutionsSystem.QRCode.Codec; 

namespace SC_SolutionsSystem.QRCode
{
    public partial class FrmQR_Reader : Form
    {
        QRCamControl camControl;
        QRProcess process;
        public Result Resultado;

        string sCamaraProceso = "";
        string sResultado = "";
        bool bDatosLeidos = false;

        Bitmap imgCamara = null; 

        public FrmQR_Reader(string CamaraProceso)
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();
            
            this.sCamaraProceso = CamaraProceso;
            this.MinimumSize = new Size(300, 300);
            this.MaximumSize = new Size(450, 450);
            this.Size = new Size(450, 450); 

            camControl = new QRCamControl();
            process = new QRProcess();
            process.ResultFound += new EventHandler<SC_SolutionsSystem.QRCode.Codec.ProcessArgs>(process_ResultFound);
            process.Start();

            camControl.SetCamera(sCamaraProceso);
            camControl.SelectedDevice.NewFrame += new NewFrameEventHandler(captureDevice_NewFrame);
            camControl.SelectedDevice.NewFrame += new NewFrameEventHandler(video_NuevoFrame);

            camControl.SelectedDevice.DesiredFrameSize = new Size(320, 240); 
            camControl.SelectedDevice.Start();
        }

        #region Form 
        private void FrmQR_Reader_Load(object sender, EventArgs e)
        {
        }

        private void FrmQR_Reader_FormClosing(object sender, FormClosingEventArgs e)
        {
            DetenerCamara(); 
        }
        #endregion Form 

        #region Funciones y Procedimientos Publicos 
        public void ShowReader()
        {
            System.Threading.Thread.Sleep(1200); 

            base.ShowDialog(); 
        }
        #endregion Funciones y Procedimientos Publicos

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

        #region Funciones y Procedimientos Privados 
        private void DetenerCamara()
        {
            if (camControl.SelectedDevice.IsRunning)
            {
                camControl.SelectedDevice.SignalToStop();
            }
            process.Stop();
        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos 
        void process_ResultFound(object sender, SC_SolutionsSystem.QRCode.Codec.ProcessArgs e)
        {
            sResultado = e.Result;
            bDatosLeidos = true;
            Resultado = process.Resultado;

            DetenerCamara(); 

            this.Close(); 
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
                            ////////// BitmapSource bmaps = Imaging.CreateBitmapSourceFromHBitmap(hBitMap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                            ////BitmapSource bmaps = Imaging.CreateBitmapSourceFromBitmap(image);
                            ////bmaps.Freeze();

                            imgCamara = image; 

                            // thMostrarImagen(); 
                            //////Dispatcher.Invoke((Action)(() =>
                            //////{
                            //////    pictureBoxMain_Base.Source = bmaps;
                            //////}), DispatcherPriority.Render, null); 




                            //////pictureBoxMain_Base.Source = bmaps;
                            ////pictureBoxMain.Image = image; 
                            ////////pictureBoxMain.Image.Save(@"C:\Test_Imagen.bmp", System.Drawing.Imaging.ImageFormat.Bmp); 

                            ////try
                            ////{
                            ////    picImagen.Image = image; 
                            ////}
                            ////catch { }

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
