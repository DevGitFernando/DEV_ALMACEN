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

    public class FileVideoSource : IVideoSource
    {
        private int bytesReceived;
        private string fileName;
        private int framesReceived;
        private bool preventFreezing;
        private ManualResetEvent stopEvent;
        private Thread thread;

        public event NewFrameEventHandler NewFrame;

        public event PlayingFinishedEventHandler PlayingFinished;

        public event VideoSourceErrorEventHandler VideoSourceError;

        public FileVideoSource()
        {
        }

        public FileVideoSource(string fileName)
        {
            this.fileName = fileName;
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
                if ((this.fileName == null) || (this.fileName == string.Empty))
                {
                    throw new ArgumentException("Video source is not specified");
                }
                this.framesReceived = 0;
                this.bytesReceived = 0;
                this.stopEvent = new ManualResetEvent(false);
                this.thread = new Thread(new ThreadStart(this.WorkerThread));
                this.thread.Name = this.fileName;
                this.thread.Start();
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
            ReasonToFinishPlaying stoppedByUser = ReasonToFinishPlaying.StoppedByUser;
            Grabber callback = new Grabber(this);
            object o = null;
            object obj3 = null;
            object obj4 = null;
            IGraphBuilder builder = null;
            IBaseFilter filter = null;
            IBaseFilter filter2 = null;
            ISampleGrabber grabber2 = null;
            IMediaControl control = null;
            IFileSourceFilter filter3 = null;
            IMediaEventEx ex = null;
            try
            {
                Type typeFromCLSID = Type.GetTypeFromCLSID(Clsid.FilterGraph);
                if (typeFromCLSID == null)
                {
                    throw new ApplicationException("Failed creating filter graph");
                }
                o = Activator.CreateInstance(typeFromCLSID);
                builder = (IGraphBuilder) o;
                typeFromCLSID = Type.GetTypeFromCLSID(Clsid.AsyncReader);
                if (typeFromCLSID == null)
                {
                    throw new ApplicationException("Failed creating filter async reader");
                }
                obj3 = Activator.CreateInstance(typeFromCLSID);
                filter = (IBaseFilter) obj3;
                ((IFileSourceFilter) obj3).Load(this.fileName, null);
                typeFromCLSID = Type.GetTypeFromCLSID(Clsid.SampleGrabber);
                if (typeFromCLSID == null)
                {
                    throw new ApplicationException("Failed creating sample grabber");
                }
                obj4 = Activator.CreateInstance(typeFromCLSID);
                grabber2 = (ISampleGrabber) obj4;
                filter2 = (IBaseFilter) obj4;
                builder.AddFilter(filter, "source");
                builder.AddFilter(filter2, "grabber");
                AMMediaType mediaType = new AMMediaType {
                    MajorType = MediaType.Video,
                    SubType = MediaSubType.RGB24
                };
                grabber2.SetMediaType(mediaType);
                if (builder.Connect(Tools.GetOutPin(filter, 0), Tools.GetInPin(filter2, 0)) < 0)
                {
                    throw new ApplicationException("Failed connecting filters");
                }
                if (grabber2.GetConnectedMediaType(mediaType) == 0)
                {
                    VideoInfoHeader header = (VideoInfoHeader) Marshal.PtrToStructure(mediaType.FormatPtr, typeof(VideoInfoHeader));
                    callback.Width = header.BmiHeader.Width;
                    callback.Height = header.BmiHeader.Height;
                    mediaType.Dispose();
                }
                if (!this.preventFreezing)
                {
                    builder.Render(Tools.GetOutPin(filter2, 0));
                    IVideoWindow window = (IVideoWindow) o;
                    window.put_AutoShow(false);
                    window = null;
                }
                grabber2.SetBufferSamples(false);
                grabber2.SetOneShot(false);
                grabber2.SetCallback(callback, 1);
                control = (IMediaControl) o;
                ex = (IMediaEventEx) o;
                control.Run();
                while (!this.stopEvent.WaitOne(0, true))
                {
                    DsEvCode code;
                    int num;
                    int num2;
                    Thread.Sleep(100);
                    if ((ex != null) && (ex.GetEvent(out code, out num, out num2, 0) >= 0))
                    {
                        ex.FreeEventParams(code, num, num2);
                        if (code == DsEvCode.Complete)
                        {
                            stoppedByUser = ReasonToFinishPlaying.EndOfStreamReached;
                            break;
                        }
                    }
                }
                control.StopWhenReady();
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
                filter = null;
                filter2 = null;
                grabber2 = null;
                control = null;
                filter3 = null;
                ex = null;
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (obj3 != null)
                {
                    Marshal.ReleaseComObject(obj3);
                    obj3 = null;
                }
                if (obj4 != null)
                {
                    Marshal.ReleaseComObject(obj4);
                    obj4 = null;
                }
            }
            if (this.PlayingFinished != null)
            {
                this.PlayingFinished(this, stoppedByUser);
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

        public bool PreventFreezing
        {
            get
            {
                return this.preventFreezing;
            }
            set
            {
                this.preventFreezing = value;
            }
        }

        public virtual string Source
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        private class Grabber : ISampleGrabberCB
        {
            private int height;
            private FileVideoSource parent;
            private int width;

            public Grabber(FileVideoSource parent)
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

