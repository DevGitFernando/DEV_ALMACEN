using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;

using ZXing;
using ZXing.QrCode;
using ZXing.Common;

using SC_SolutionsSystem.QRCode.Codec.Data;
////using SC_SolutionsSystem.QRCode.Codec.Data.QRCodeImage;
////using SC_SolutionsSystem.QRCode.Codec.Data.QRCodeBitmapImage; 

namespace SC_SolutionsSystem.QRCode.Codec
{
    /// <summary> 
    /// Class to pass arguments between QRProcess events. 
    /// </summary> 
    internal class ProcessArgs : EventArgs
    {
        /// <summary> 
        /// Gets or sets the result. 
        /// </summary> 
        /// <value> 
        /// The result. 
        /// </value> 
        public string Result { get; set; }
    }

    internal class QRProcess
    {
        QRCodeDecoder reader_aux = new QRCodeDecoder(); 

        public QRCodeReader reader = new QRCodeReader();
        public BarcodeReader reader_GN = new BarcodeReader();
        public Result Resultado;
        IList<BarcodeFormat> list;

        public Bitmap CurrentBitmap { get; private set; }
        public event EventHandler<ProcessArgs> ResultFound;
        public bool Run { get; set; }
        public bool newBitmap { get; set; }

        public QRProcess()
        {
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
                        while (this.newBitmap == false)
                        {
                            Monitor.Wait(this, 1000);
                        }

                        using (Bitmap bitmap = (Bitmap)this.CurrentBitmap.Clone())
                        {
                            Monitor.Exit(this);
                            var result = this.Process(bitmap);
                            string sResult = (string)result;

                            /////// Revisar 
                            //// if (!string.IsNullOrWhiteSpace(result))
                            if (sResult != "")
                            {
                                if (this.ResultFound != null)
                                {
                                    ProcessArgs args = new ProcessArgs() { Result = result };
                                    this.ResultFound(this, args);
                                    this.Run = false;
                                    // Console.Beep(220, (1000 * 1));
                                    Console.Beep(); 
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
        public string Process(Bitmap bitmap)
        {
            string sRegresa = "";
            ////string sRegresa_Aux = "";
            try
            {
                //////////SC_SolutionsSystem.ZXing.LuminanceSource source = new RGBLuminanceSource(bitmap, bitmap.Width, bitmap.Height);
                //////////var binarizer = new HybridBinarizer(source);
                //////////var binBitmap = new BinaryBitmap(binarizer);

                //////////sRegresa = reader.decode(binBitmap, BarcodeFormat.QR_CODE).Text;

                //////////sRegresa = sRegresa; 


                ////reader_GN.Options.PossibleFormats = list; 
                Resultado = reader_GN.Decode(bitmap);
                if (Resultado != null)
                {
                    sRegresa = Resultado.Text; 
                }

                ////if (sRegresa != "")
                ////{
                ////    //reader.Imagen = bitmap;
                ////    sRegresa_Aux = reader_aux.Decode(new QRCodeBitmapImage(new Bitmap(bitmap)));
                ////}
            }
            catch
            {
            }

            return sRegresa; 
        }
    }
}
