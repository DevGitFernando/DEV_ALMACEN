using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO; 

using AForge.Video;
using AForge.Imaging;
using AForge.Video.DirectShow;

using ZXing;
using SC_SolutionsSystem.Errores; 
using SC_SolutionsSystem.QRCode.Codec; 

namespace SC_SolutionsSystem.QRCode
{
    public partial class FrmCamDigitalizar : Form
    {
        QRCamControl camControl;
        QRProcess process;
        public Result Resultado;

        string sCamaraProceso = "";
        string sResultado = "";
        bool bDatosLeidos = false;

        Bitmap imgCamara = null;
        bool bImagenDigitalizada = false;
        string sImg_Original = "";
        string sImg_Comprimida = ""; 

        string sDirectorio = Application.StartupPath + @"\Digitalizacion";
        string sFileName = "Digitalizacion";
        string sFileNameCompress = "Digitalizacion";
        string sMarcaDeTiempo = ""; 

        public FrmCamDigitalizar(string CamaraProceso)
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            //int iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.80);
            //int iAltoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Height * 0.70);

            Size tam = new Size(1280, 720);
            //tam = new Size(iAnchoPantalla, iAltoPantalla);

            this.sCamaraProceso = CamaraProceso;



            if (!Directory.Exists(sDirectorio))
            {
                Directory.CreateDirectory(sDirectorio); 
            }

            sMarcaDeTiempo = MarcaDeTiempo();
            sFileName += "__" + sMarcaDeTiempo;
            sFileName = Path.Combine(sDirectorio, sFileName + ".png");

            sFileNameCompress += "__" + sMarcaDeTiempo + "__Compress";
            sFileNameCompress = Path.Combine(sDirectorio, sFileNameCompress + ".png");
            


            camControl = new QRCamControl();
            //process = new QRProcess();
            //process.ResultFound += new EventHandler<SC_SolutionsSystem.QRCode.Codec.ProcessArgs>(process_ResultFound);
            //process.Start();

            camControl.SetCamera(sCamaraProceso);
            //camControl.SelectedDevice.NewFrame += new NewFrameEventHandler(captureDevice_NewFrame);
            camControl.SelectedDevice.NewFrame += new NewFrameEventHandler(video_NuevoFrame);


            //tam = camControl.BestResolution;
            //iAnchoPantalla = (int)((double)tam.Width * 0.70);
            //iAltoPantalla = (int)((double)tam.Height * 0.85);


            //iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.80);
            //iAltoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Height * 0.70);

            //this.Size = tam;
            this.Size = new Size(this.Width, this.Height);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            camControl.SelectedDevice.DesiredFrameSize = tam; // new Size(800, 600); 
            //camControl.SelectedDevice.DesiredFrameSize = camControl.BestResolution; 
            camControl.SelectedDevice.Start();

            btnNuevo.Enabled = false; 
        }

        private string MarcaDeTiempo()
        {
            string sRegresa = "";
            DateTime dt = DateTime.Now;

            sRegresa = string.Format("{0}{1}{2}{3}{4}{5}",
                dt.Year.ToString("0000"), dt.Month.ToString("00"), dt.Day.ToString("00"),
                dt.Hour.ToString("00"), dt.Minute.ToString("00"), dt.Second.ToString("00")
                );

            return sRegresa; 
        }

        #region Form 
        private void FrmCamDigitalizar_Load(object sender, EventArgs e)
        {
        }

        private void FrmCamDigitalizar_FormClosing(object sender, FormClosingEventArgs e)
        {
            DetenerCamara(); 
        }

        private void FrmCamDigitalizar_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F4:
                    btnNuevo_Click(null, null); 
                    break;

                case Keys.F6:
                    btnCapturarImagen_Click(null, null); 
                    break; 

                case Keys.F12:
                    btnDigitalizar_Click(null, null); 
                    break; 
            }
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

        #region Funciones y Procedimientos Privados 
        private void DetenerCamara()
        {
            if (camControl.SelectedDevice.IsRunning)
            {
                camControl.SelectedDevice.SignalToStop();
            }
            //process.Stop();
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
            imgCamara = Imagen; 
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

                            ////process.NewBitmap(image);
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            camControl.SelectedDevice.Start();
            btnCapturarImagen.Enabled = true;
            btnNuevo.Enabled = false; 
        }

        private void btnCapturarImagen_Click(object sender, EventArgs e)
        {
            camControl.SelectedDevice.Stop();
            btnCapturarImagen.Enabled = false;
            btnNuevo.Enabled = true; 
        }

        private void btnDigitalizar_Click(object sender, EventArgs e)
        {
            bImagenDigitalizada = false; 

            try
            {
                camControl.SelectedDevice.Stop();
                Thread.Sleep(100);
                Application.DoEvents(); 

                using (Bitmap img = imgCamara)
                {
                    img.Save(sFileName, ImageFormat.Png);
                }

                using (System.Drawing.Image img = System.Drawing.Image.FromFile(sFileName))
                {
                    imgCamara = (Bitmap)img; 
                }

                bImagenDigitalizada = true;
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(string.Format("Función: {0}  Mensaje: {1}", "FrmCamDigitalizar.btnDigitalizar_Click", ex.Message));
                camControl.SelectedDevice.Start(); 
            }

            if (bImagenDigitalizada)
            {
                this.Hide();
            }
        }
    }
}
