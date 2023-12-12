using System;
using System.Collections.Generic;
using System.Drawing; 
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Text;

using Vintasoft.Barcode;

namespace DllFarmaciaSoft.QRCode
{
    public class clsDataMatrix
    {
        public float bcLength = 2f;
        private BarcodeWriter ICBB;

        string sLastError = ""; 

        public clsDataMatrix()
        {
            BarcodeGlobalSettings.RegisterBarcodeWriter("Gabriel Jimenez", "gabriel.jimenez@induxsoft.net", "d8mMRgA5n341H9w8Kk5DzZhYO2Hk7pumwtjfZYNLf0OwicPdboa6nz6mvmmkR+fF6aD1PH0uhRh/uJFKGGB+L8nYe+mug6Izqa+U1KF0WIiAAHwJBKWLLxS1t2yPth2w/JCkQrW2CtDvkDgTnYXQTcQdmiQ0gQAozTChCXfTeGSo");           
        }

        private bool generarImagen(string Codigo, string Ruta, BarcodeType Formato) 
        {
            bool bRegresa = false; 
            try
            {
                UnitOfMeasure measure = UnitOfMeasure.Centimeters;

                this.ICBB = new BarcodeWriter();
                this.ICBB.Settings.Barcode = Formato;
                this.ICBB.Settings.Value = Codigo;
                this.ICBB.Settings.Resolution = 200f;
                ////this.ICBB.Settings.ForeColor = Color.Gray;
                ////this.ICBB.Settings.DataMatrixSymbol = Vintasoft.Barcode.BarcodeInfo.DataMatrixSymbolType.Row24Col24;
                ////this.ICBB.Settings.ForeColor = Color.White;
                ////this.ICBB.Settings.BackColor = Color.Black;

                this.ICBB.Settings.SetWidth(this.bcLength, measure);
                this.ICBB.GetBarcodeAsBitmap().Save(Ruta, ImageFormat.Jpeg);
                bRegresa = true;
            }
            catch (Exception ex)
            {
                ////General.msjError("Error : " + exception);
                bRegresa = false;
                sLastError = ex.Message;
            }

            return bRegresa;
        }

        public bool CodificarImagen(string UUID, string Ruta)
        {
            return CodificarImagen(UUID, Ruta, BarcodeType.DataMatrix); 
        }

        public bool CodificarImagen__CodeBar128(string UUID, string Ruta)
        {
            return CodificarImagen(UUID, Ruta, BarcodeType.Code128);
        }

        public bool CodificarImagen(string UUID, string Ruta, BarcodeType Formato)
        {
            bool bRegresa = false;

            sLastError = ""; 
            try
            {
                bRegresa = this.generarImagen(UUID, Ruta, Formato);
            }
            catch (Exception ex)
            {
                //General.msjError("Error : " + exception);                
                sLastError = ex.Message;
            }

            return bRegresa;
        }
    }
}
