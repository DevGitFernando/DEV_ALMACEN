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
    /// <summary> 
    /// Class to pass arguments between ReaderProcess events. 
    /// </summary> 
    public class ProcessArgs : EventArgs
    {
        /// <summary> 
        /// Gets or sets the result. 
        /// </summary> 
        /// <value> 
        /// The result. 
        /// </value> 
        public Result Resultado { get; set; }
    }

    public class ReaderProcess
    {
        private BarcodeReader reader_GN = new BarcodeReader();
        private Result rsLecturaResultado; 
        IList<BarcodeFormat> list;
        private BarcodeFormat formatoDeCodigo = BarcodeFormat.NONE; 

        public Bitmap CurrentBitmap { get; private set; }
        public event EventHandler<ProcessArgs> ResultFound;
        public bool Run { get; set; }
        public bool newBitmap { get; set; }

        public ReaderProcess(BarcodeFormat Formato)
        {
            this.formatoDeCodigo = Formato; 

            list = new List<BarcodeFormat>();
            list.Add(BarcodeFormat.All_1D);

            list.Add(BarcodeFormat.All_1D);
            list.Add(BarcodeFormat.AZTEC);
            list.Add(BarcodeFormat.CODABAR);
            list.Add(BarcodeFormat.CODE_128);
            list.Add(BarcodeFormat.CODE_39);
            list.Add(BarcodeFormat.CODE_93);
            list.Add(BarcodeFormat.DATA_MATRIX);
            list.Add(BarcodeFormat.EAN_13);
            list.Add(BarcodeFormat.EAN_8);
            list.Add(BarcodeFormat.ITF);
            list.Add(BarcodeFormat.MAXICODE);
            list.Add(BarcodeFormat.MSI);
            list.Add(BarcodeFormat.PDF_417);
            list.Add(BarcodeFormat.PLESSEY);
            list.Add(BarcodeFormat.QR_CODE);
            list.Add(BarcodeFormat.RSS_14);
            list.Add(BarcodeFormat.RSS_EXPANDED);
            list.Add(BarcodeFormat.UPC_A);
            list.Add(BarcodeFormat.UPC_E);
            list.Add(BarcodeFormat.UPC_EAN_EXTENSION);
        }

        /// <summary> 
        /// When a new bitmap is available, call this method to prepare the bitmap for be consumed.
        /// </summary> 
        /// <param name="newBitmap">The bitmap to be consumed.</param>
        public void NewBitmap(Bitmap newBitmap)
        {
            Monitor.Enter(this);
            if (this.newBitmap == false)
            {
                this.CurrentBitmap = (Bitmap)newBitmap.Clone();
                this.newBitmap = true;

                Monitor.PulseAll(this);

            }
            Monitor.Exit(this);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary> 
        public void Stop()
        {
            Monitor.Enter(this);
            this.Run = false;
            Monitor.PulseAll(this);
            Monitor.Exit(this);
        }

        /// <summary> 
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this.Run = true;

            try
            {
                ThreadStart start = new ThreadStart
                (
                delegate
                {
                    while (this.Run)
                    {
                        Monitor.Enter(this);
                        while (this.newBitmap == false && this.Run)
                        {
                            Monitor.Wait(this, 1000);
                        }

                        if (this.CurrentBitmap != null)
                        {
                            using (Bitmap bitmap = (Bitmap)this.CurrentBitmap.Clone())
                            {
                                Monitor.Exit(this);
                                Result result = this.Process(bitmap);
                                ////string sResult = result.Text;

                                /////// Revisar  
                                if (result != null)
                                {
                                    if (this.ResultFound != null)
                                    {
                                        Console.Beep();
                                        ProcessArgs args = new ProcessArgs() { Resultado = result };
                                        this.ResultFound(this, args);

                                        //// Esperar a que se quite el objeto del lector 
                                        Thread.Sleep(100);

                                        ////this.Run = false; //Mantener activo el lector 
                                        // Console.Beep(220, (1000 * 1));
                                    }
                                }
                            }
                        }

                        // the class is ready to accept a new bitmap to evaluate. 
                        this.newBitmap = false;

                    }

                }
                );

                Thread t = new Thread(start);
                t.Start();
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }
        }

        /// <summary> 
        /// Processes the specified bitmap. 
        /// </summary> 
        /// <param name="bitmap">The bitmap.</param> 
        /// <returns>The message decoded or string.empty</returns> 
        public Result Process(Bitmap bitmap)
        {
            string sRegresa = "";

            try
            {
                ////reader_GN.Options.PossibleFormats = list; 
                rsLecturaResultado = reader_GN.Decode(bitmap);
                if (rsLecturaResultado != null)
                {
                    if (formatoDeCodigo == BarcodeFormat.NONE)
                    {
                        sRegresa = rsLecturaResultado.Text;
                    }
                    else
                    {
                        if (formatoDeCodigo == rsLecturaResultado.BarcodeFormat)
                        {
                            sRegresa = rsLecturaResultado.Text;
                        }
                        else
                        {
                            rsLecturaResultado = null; 
                        }
                    }
                }
            }
            catch
            {
            }

            return rsLecturaResultado; 
        }
    }
}
