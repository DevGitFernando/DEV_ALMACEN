namespace AForge.Video.DirectShow
{
    using AForge.Video;
    using AForge.Video.DirectShow.Internals;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class VideoCaptureDevice : IVideoSource
    {
        private int bytesReceived;
        private int desiredFrameRate;
        private Size desiredFrameSize;
        private string deviceMoniker;
        private int framesReceived;
        private bool needToDisplayPropertyPage;
        private IntPtr parentWindowForPropertyPage;
        private ManualResetEvent stopEvent;
        private Thread thread;
        private AForge.Video.DirectShow.VideoCapabilities[] videoCapabilities;

        public event NewFrameEventHandler NewFrame;

        public event PlayingFinishedEventHandler PlayingFinished;

        public event VideoSourceErrorEventHandler VideoSourceError;

        public VideoCaptureDevice()
        {
            this.desiredFrameSize = new Size(0, 0);
            this.parentWindowForPropertyPage = IntPtr.Zero;
        }

        public VideoCaptureDevice(string deviceMoniker)
        {
            this.desiredFrameSize = new Size(0, 0);
            this.parentWindowForPropertyPage = IntPtr.Zero;
            this.deviceMoniker = deviceMoniker;
        }

        public void DisplayPropertyPage(IntPtr parentWindow)
        {
            if ((this.deviceMoniker == null) || (this.deviceMoniker == string.Empty))
            {
                throw new ArgumentException("Video source is not specified");
            }
            lock (this)
            {
                object ppUnk = null;
                try
                {
                    ppUnk = AForge.Video.DirectShow.FilterInfo.CreateFilter(this.deviceMoniker);
                }
                catch
                {
                }
                if (ppUnk == null)
                {
                    if (!this.IsRunning)
                    {
                        throw new ApplicationException("Failed creating device object for moniker.");
                    }
                    this.parentWindowForPropertyPage = parentWindow;
                    this.needToDisplayPropertyPage = true;
                }
                else
                {
                    CAUUID cauuid;
                    if (!(ppUnk is ISpecifyPropertyPages))
                    {
                        throw new NotSupportedException("The video source does not support configuration property page.");
                    }
                    ((ISpecifyPropertyPages) ppUnk).GetPages(out cauuid);
                    AForge.Video.DirectShow.FilterInfo info = new AForge.Video.DirectShow.FilterInfo(this.deviceMoniker);
                    Win32.OleCreatePropertyFrame(parentWindow, 0, 0, info.Name, 1, ref ppUnk, cauuid.cElems, cauuid.pElems, 0, 0, IntPtr.Zero);
                    Marshal.FreeCoTaskMem(cauuid.pElems);
                    Marshal.ReleaseComObject(ppUnk);
                }
            }
        }

        private void Free()
        {
            this.thread = null;
            this.stopEvent.Close();
            this.stopEvent = null;
        }

        protected void OnNewFrame(Bitmap image)
        {
            this.framesReceived++;
            if (!this.stopEvent.WaitOne(0, true) && (this.NewFrame != null))
            {
                this.NewFrame(this, new NewFrameEventArgs(image));
            }
        }

        public void SignalToStop()
        {
            if (this.thread != null)
            {
                this.stopEvent.Set();
            }
        }

        public void Start()
        {
            if (this.thread == null)
            {
                if ((this.deviceMoniker == null) || (this.deviceMoniker == string.Empty))
                {
                    throw new ArgumentException("Video source is not specified");
                }
                this.framesReceived = 0;
                this.bytesReceived = 0;
                this.stopEvent = new ManualResetEvent(false);
                lock (this)
                {
                    this.thread = new Thread(new ThreadStart(this.WorkerThread));
                    this.thread.Name = this.deviceMoniker;
                    this.thread.Start();
                }
            }
        }

        public void Stop()
        {
            if (this.IsRunning)
            {
                this.thread.Abort();
                this.WaitForStop();
            }
        }

        public void WaitForStop()
        {
            if (this.thread != null)
            {
                this.thread.Join();
                this.Free();
            }
        }

        private void WorkerThread()
        {
            this.WorkerThread(true);
        }

        private void WorkerThread(bool runGraph)
        {
            Grabber callback = new Grabber(this);
            object o = null;
            object obj3 = null;
            object ppUnk = null;
            object obj5 = null;
            ICaptureGraphBuilder2 builder = null;
            IFilterGraph2 graph = null;
            IBaseFilter filter = null;
            IBaseFilter filter2 = null;
            ISampleGrabber grabber2 = null;
            IMediaControl control = null;
            try
            {
                Type typeFromCLSID = Type.GetTypeFromCLSID(Clsid.CaptureGraphBuilder2);
                if (typeFromCLSID == null)
                {
                    throw new ApplicationException("Failed creating capture graph builder");
                }
                o = Activator.CreateInstance(typeFromCLSID);
                builder = (ICaptureGraphBuilder2) o;
                typeFromCLSID = Type.GetTypeFromCLSID(Clsid.FilterGraph);
                if (typeFromCLSID == null)
                {
                    throw new ApplicationException("Failed creating filter graph");
                }
                obj3 = Activator.CreateInstance(typeFromCLSID);
                graph = (IFilterGraph2) obj3;
                builder.SetFiltergraph((IGraphBuilder) graph);
                ppUnk = AForge.Video.DirectShow.FilterInfo.CreateFilter(this.deviceMoniker);
                if (ppUnk == null)
                {
                    throw new ApplicationException("Failed creating device object for moniker");
                }
                filter = (IBaseFilter) ppUnk;
                typeFromCLSID = Type.GetTypeFromCLSID(Clsid.SampleGrabber);
                if (typeFromCLSID == null)
                {
                    throw new ApplicationException("Failed creating sample grabber");
                }
                obj5 = Activator.CreateInstance(typeFromCLSID);
                grabber2 = (ISampleGrabber) obj5;
                filter2 = (IBaseFilter) obj5;
                graph.AddFilter(filter, "source");
                graph.AddFilter(filter2, "grabber");
                AMMediaType mediaType = new AMMediaType {
                    MajorType = MediaType.Video,
                    SubType = MediaSubType.RGB24
                };
                grabber2.SetMediaType(mediaType);
                grabber2.SetBufferSamples(false);
                grabber2.SetOneShot(false);
                grabber2.SetCallback(callback, 1);
                if ((this.desiredFrameRate != 0) || ((this.desiredFrameSize.Width != 0) && (this.desiredFrameSize.Height != 0)))
                {
                    object obj6;
                    builder.FindInterface(PinCategory.Capture, MediaType.Video, filter, typeof(IAMStreamConfig).GUID, out obj6);
                    if (obj6 != null)
                    {
                        IAMStreamConfig videoStreamConfig = (IAMStreamConfig) obj6;
                        if (this.videoCapabilities == null)
                        {
                            try
                            {
                                this.videoCapabilities = AForge.Video.DirectShow.VideoCapabilities.FromStreamConfig(videoStreamConfig);
                            }
                            catch
                            {
                            }
                        }
                        videoStreamConfig.GetFormat(out mediaType);
                        VideoInfoHeader structure = (VideoInfoHeader) Marshal.PtrToStructure(mediaType.FormatPtr, typeof(VideoInfoHeader));
                        if ((this.desiredFrameSize.Width != 0) && (this.desiredFrameSize.Height != 0))
                        {
                            structure.BmiHeader.Width = this.desiredFrameSize.Width;
                            structure.BmiHeader.Height = this.desiredFrameSize.Height;
                        }
                        if (this.desiredFrameRate != 0)
                        {
                            structure.AverageTimePerFrame = 0x989680 / this.desiredFrameRate;
                        }
                        Marshal.StructureToPtr(structure, mediaType.FormatPtr, false);
                        videoStreamConfig.SetFormat(mediaType);
                        mediaType.Dispose();
                    }
                }
                else if (this.videoCapabilities == null)
                {
                    object obj7;
                    builder.FindInterface(PinCategory.Capture, MediaType.Video, filter, typeof(IAMStreamConfig).GUID, out obj7);
                    if (obj7 != null)
                    {
                        IAMStreamConfig config2 = (IAMStreamConfig) obj7;
                        try
                        {
                            this.videoCapabilities = AForge.Video.DirectShow.VideoCapabilities.FromStreamConfig(config2);
                        }
                        catch
                        {
                        }
                    }
                }
                if (runGraph)
                {
                    builder.RenderStream(PinCategory.Capture, MediaType.Video, filter, null, filter2);
                    if (grabber2.GetConnectedMediaType(mediaType) == 0)
                    {
                        VideoInfoHeader header2 = (VideoInfoHeader) Marshal.PtrToStructure(mediaType.FormatPtr, typeof(VideoInfoHeader));
                        callback.Width = header2.BmiHeader.Width;
                        callback.Height = header2.BmiHeader.Height;
                        mediaType.Dispose();
                    }
                    control = (IMediaControl) obj3;
                    control.Run();
                    while (!this.stopEvent.WaitOne(0, true))
                    {
                        Thread.Sleep(100);
                        if (this.needToDisplayPropertyPage)
                        {
                            this.needToDisplayPropertyPage = false;
                            try
                            {
                                CAUUID cauuid;
                                ((ISpecifyPropertyPages) ppUnk).GetPages(out cauuid);
                                AForge.Video.DirectShow.FilterInfo info = new AForge.Video.DirectShow.FilterInfo(this.deviceMoniker);
                                Win32.OleCreatePropertyFrame(this.parentWindowForPropertyPage, 0, 0, info.Name, 1, ref ppUnk, cauuid.cElems, cauuid.pElems, 0, 0, IntPtr.Zero);
                                Marshal.FreeCoTaskMem(cauuid.pElems);
                                continue;
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    control.StopWhenReady();
                }
            }
            catch (Exception exception)
            {
                if (this.VideoSourceError != null)
                {
                    this.VideoSourceError(this, new VideoSourceErrorEventArgs(exception.Message));
                }
            }
            finally
            {
                builder = null;
                graph = null;
                filter = null;
                filter2 = null;
                grabber2 = null;
                control = null;
                if (obj3 != null)
                {
                    Marshal.ReleaseComObject(obj3);
                    obj3 = null;
                }
                if (ppUnk != null)
                {
                    Marshal.ReleaseComObject(ppUnk);
                    ppUnk = null;
                }
                if (obj5 != null)
                {
                    Marshal.ReleaseComObject(obj5);
                    obj5 = null;
                }
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
            }
            if (this.PlayingFinished != null)
            {
                this.PlayingFinished(this, ReasonToFinishPlaying.StoppedByUser);
            }
        }

        public int BytesReceived
        {
            get
            {
                int bytesReceived = this.bytesReceived;
                this.bytesReceived = 0;
                return bytesReceived;
            }
        }

        public int DesiredFrameRate
        {
            get
            {
                return this.desiredFrameRate;
            }
            set
            {
                this.desiredFrameRate = value;
            }
        }

        public Size DesiredFrameSize
        {
            get
            {
                return this.desiredFrameSize;
            }
            set
            {
                this.desiredFrameSize = value;
            }
        }

        public int FramesReceived
        {
            get
            {
                int framesReceived = this.framesReceived;
                this.framesReceived = 0;
                return framesReceived;
            }
        }

        public bool IsRunning
        {
            get
            {
                if (this.thread != null)
                {
                    if (!this.thread.Join(0))
                    {
                        return true;
                    }
                    this.Free();
                }
                return false;
            }
        }

        public virtual string Source
        {
            get
            {
                return this.deviceMoniker;
            }
            set
            {
                this.deviceMoniker = value;
            }
        }

        public AForge.Video.DirectShow.VideoCapabilities[] VideoCapabilities
        {
            get
            {
                if ((this.videoCapabilities == null) && !this.IsRunning)
                {
                    this.WorkerThread(false);
                }
                return this.videoCapabilities;
            }
        }

        private class Grabber : ISampleGrabberCB
        {
            private int height;
            private VideoCaptureDevice parent;
            private int width;

            public Grabber(VideoCaptureDevice parent)
            {
                this.parent = parent;
            }

            public int BufferCB(double sampleTime, IntPtr buffer, int bufferLen)
            {
                if (this.parent.NewFrame != null)
                {
                    Bitmap image = new Bitmap(this.width, this.height, PixelFormat.Format24bppRgb);
                    BitmapData bitmapdata = image.LockBits(new Rectangle(0, 0, this.width, this.height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    int stride = bitmapdata.Stride;
                    int num2 = bitmapdata.Stride;
                    int dst = bitmapdata.Scan0.ToInt32() + (num2 * (this.height - 1));
                    int src = buffer.ToInt32();
                    for (int i = 0; i < this.height; i++)
                    {
                        Win32.memcpy(dst, src, stride);
                        dst -= num2;
                        src += stride;
                    }
                    image.UnlockBits(bitmapdata);
                    this.parent.OnNewFrame(image);
                    image.Dispose();
                }
                return 0;
            }

            public int SampleCB(double sampleTime, IntPtr sample)
            {
                return 0;
            }

            public int Height
            {
                get
                {
                    return this.height;
                }
                set
                {
                    this.height = value;
                }
            }

            public int Width
            {
                get
                {
                    return this.width;
                }
                set
                {
                    this.width = value;
                }
            }
        }
    }
}

