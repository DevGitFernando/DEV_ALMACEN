using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing; 
using System.Windows.Forms; 

using AForge.Video.DirectShow;

namespace ZXing
{
    /// <summary> 
    /// Class to control the cameras of the system. 
    /// </summary> 
    internal class ReaderCamControl
    {
        private int selectedFrameSize;
        private int selectedFps;

        private VideoCaptureDevice selectedDevice;
        public FilterInfoCollection Devices { get; private set; }
        public Dictionary<int, string> FrameSizes { get; private set; }
        public List<int> Fps;
        private Dictionary<string, int> listaCamaras = new Dictionary<string, int>();
        public List<string> lstFrameSizes = new List<string>();

        /// <summary> 
        /// Gets the selected device. 
        /// </summary> 
        public VideoCaptureDevice SelectedDevice
        {
            get { return selectedDevice; }
            private set
            {
                selectedDevice = value;
                RefreshFrameSize();
            }
        }

        /// <summary> 
        /// Initializes a new instance of the <see cref="CamControl"/> class. 
        /// </summary> 
        public ReaderCamControl()
        {
            string sDevice = "";
            listaCamaras = new Dictionary<string, int>();
            Devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            for (int i = 0; i < Devices.Count; i++)
            {
                sDevice = Devices[i].Name;
                listaCamaras.Add(sDevice.ToUpper(), i);
            }

            //// by default select the first one
            SetCamera(0);
        }

        /// <summary> 
        /// Sets the active camera. 
        /// </summary> 
        /// <param name="index">The index of the camera</param> 
        public void SetCamera(int index)
        {
            try
            {
                if (Devices.Count < index)
                {
                    throw new IndexOutOfRangeException("Dispositivo invalido");
                }

                SelectedDevice = new VideoCaptureDevice(Devices[index].MonikerString);
            }
            catch 
            { 
            }
        }

        public void SetCamera(string Dispositivo)
        {
            try
            {
                int iDispositivo = 0;
                Dispositivo = Dispositivo.ToUpper();

                if (listaCamaras.ContainsKey(Dispositivo))
                {
                    iDispositivo = listaCamaras[Dispositivo];
                    this.SetCamera(iDispositivo);
                }
            }
            catch { }
        }

        /// <summary> 
        /// Sets the size of the frame. 
        /// </summary> 
        /// <param name="index">The index of the available fps</param> 
        public void SetFrameSize(int index)
        {
            if (FrameSizes.Count < index)
            {
                throw new IndexOutOfRangeException("Framesize fuera de rango " + index);
            }

            selectedFrameSize = index;
            RefreshFps();
            ConfigureCamera();
        }

        /// <summary> 
        /// Sets the FPS of the active camera. 
        /// </summary> 
        /// <param name="fps">The FPS</param>
        public void SetFps(int fps)
        {
            if (!Fps.Contains(fps))
            {
                throw new IndexOutOfRangeException("Fps inválidos " + fps);
            }

            selectedFps = fps;
            ConfigureCamera();
        }


        /// <summary> 
        /// Refreshes the size of the frame. 
        /// </summary> 
        private void RefreshFrameSize()
        {
            this.FrameSizes = new Dictionary<int, string>();
            lstFrameSizes = lstFrameSizes = new List<string>();
            int i = 0;

            foreach (VideoCapabilities set in SelectedDevice.VideoCapabilities)
            {
                this.FrameSizes.Add(i, String.Format("{0:000000} x {1:000000}", set.FrameSize.Width, set.FrameSize.Height));
                lstFrameSizes.Add(String.Format("{0:000000}|{1:000000}|{2}", set.FrameSize.Width, set.FrameSize.Height, i)); 
                i++; 
            }

            selectedFrameSize = 0;
            RefreshFps();
        }

        public void SetMediumResolution()
        {
            int MaxFramerate = selectedDevice.VideoCapabilities[selectedFrameSize].FrameRate;
            List<string> sLista = new List<string>();
            int iAncho = 0;
            int iAlto = 0;
            Size tam = new Size();
            string[] sValores = null;
            double iValues = lstFrameSizes.Count % 2;
            int iValue = lstFrameSizes.Count/2;
            string sResolution = "";

            iValue = iValue + (int)iValues; //// == 0 ? iValue : iValue + 1; 
            iValue--; 

            sLista = lstFrameSizes; 
            sLista.Sort();

            ////foreach (string sResolution in sLista)
            {
                sResolution = sLista[iValue]; 
                sValores = sResolution.Split('|');
                iAncho = Convert.ToInt32(sValores[0]);
                iAlto = Convert.ToInt32(sValores[1]); 

                tam = new Size(iAncho, iAlto); 
                ///break;
            }

            ConfigureCamera(tam); 
        }

        /// <summary>
        /// Refreshes the FPS. 
        /// </summary>  
        private void RefreshFps()
        {
            int MaxFramerate = selectedDevice.VideoCapabilities[selectedFrameSize].FrameRate;
            Fps = new List<int>();
            List<int> fps_aux = new List<int>();

            for (int i = 1; i < MaxFramerate; i++)
            {
                if (i % 5 == 0)
                {
                    Fps.Add(i);
                }
            }

            // selectedFps = Fps.Min();
            fps_aux = Fps;
            fps_aux.Sort();

            try
            {
                selectedFps = fps_aux[0];
            }
            catch { }
        }

        private void ConfigureCamera()
        {
            ConfigureCamera(SelectedDevice.VideoCapabilities[selectedFrameSize].FrameSize); 
        }

        /// <summary> 
        /// Configures the camera. 
        /// </summary> 
        public void ConfigureCamera(Size FrameSize)
        {
            ////SelectedDevice.DesiredFrameSize = SelectedDevice.VideoCapabilities[selectedFrameSize].FrameSize;
            ////SelectedDevice.DesiredFrameRate = selectedFps;

            SelectedDevice.DesiredFrameSize = FrameSize;
            SelectedDevice.DesiredFrameRate = selectedFps;
        }

    } 
}
