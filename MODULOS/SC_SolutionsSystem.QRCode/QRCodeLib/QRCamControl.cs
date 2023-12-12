using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing; 

using AForge.Video.DirectShow;

namespace SC_SolutionsSystem.QRCode.Codec
{
    /// <summary> 
    /// Class to control the cameras of the system. 
    /// </summary> 
    internal class QRCamControl
    {
        private int selectedFrameSize;
        private int selectedFps;

        private VideoCaptureDevice selectedDevice;
        public FilterInfoCollection Devices { get; private set; }
        public Dictionary<int, string> FrameSizes { get; private set; }
        public List<int> Fps;
        public ResolutionList Resoluciones; 
        private Dictionary<string, int> listaCamaras = new Dictionary<string, int>(); 

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
        public QRCamControl()
        {
            string sDevice = "";
            listaCamaras = new Dictionary<string, int>();
            Devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            for (int i = 0; i < Devices.Count; i++)
            {
                sDevice = Devices[i].Name;
                listaCamaras.Add(sDevice.ToUpper(), i);
            }

            // by default select the first one
            SetCamera(0);
        }

        public Size BestResolution
        {
            get 
            {
                Size resolution = new Size(800, 600);

                if (Resoluciones != null)
                {
                    if (Resoluciones.Count > 0)
                    {
                        Resolution ret = new Resolution();
                        ret = Resoluciones[Resoluciones.Count - 1];

                        resolution.Width = ret.Width;
                        resolution.Height = ret.Height; 
                    }
                }

                return resolution; 
            }
        }

        /// <summary> 
        /// Sets the active camera. 
        /// </summary> 
        /// <param name="index">The index of the camera</param> 
        public void SetCamera(int index)
        {
            if (Devices.Count < index)
            {
                throw new IndexOutOfRangeException("Dispositivo invalido");
            }

            SelectedDevice = new VideoCaptureDevice(Devices[index].MonikerString);
        }

        public void SetCamera(string Dispositivo)
        {
            int iDispositivo = 0;
            Dispositivo = Dispositivo.ToUpper(); 

            if (listaCamaras.ContainsKey(Dispositivo))
            {
                iDispositivo = listaCamaras[Dispositivo];
                this.SetCamera(iDispositivo); 
            } 
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
            Resoluciones = new ResolutionList(); 
            Resolution item = new Resolution(); 
            string sResolucion = ""; 
            int i = 0;            

            foreach (VideoCapabilities set in SelectedDevice.VideoCapabilities)
            {
                sResolucion = String.Format("{0:6} x {1:6}", set.FrameSize.Width, set.FrameSize.Height);
                this.FrameSizes.Add(i, sResolucion);

                item = new Resolution(set.FrameSize.Width, set.FrameSize.Height); 
                Resoluciones.AddIfNew(item); 
                i++;        
            }

            Resoluciones.Sort();
            Resoluciones.Sort();  

            selectedFrameSize = 0;
            selectedFrameSize = Resoluciones.Count - 1; 
            RefreshFps();
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

        /// <summary> 
        /// Configures the camera. 
        /// </summary> 
        private void ConfigureCamera()
        {
            SelectedDevice.DesiredFrameSize = SelectedDevice.VideoCapabilities[selectedFrameSize].FrameSize;
            SelectedDevice.DesiredFrameRate = selectedFps;
        }

    } 
}
