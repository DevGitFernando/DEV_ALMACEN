using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Text;

using Vintasoft.Barcode;

using SC_SolutionsSystem;
using SC_SolutionsSystem.QRCode; 
using SC_SolutionsSystem.QRCode.Codec;

namespace Dll_IFacturacion.CFDI.geCBB
{
    public class clsCBB
    {
        public float bcLength = 3f;
        private BarcodeWriter ICBB;

        public clsCBB()
        {
            BarcodeGlobalSettings.RegisterBarcodeWriter("Gabriel Jimenez", "gabriel.jimenez@induxsoft.net", "d8mMRgA5n341H9w8Kk5DzZhYO2Hk7pumwtjfZYNLf0OwicPdboa6nz6mvmmkR+fF6aD1PH0uhRh/uJFKGGB+L8nYe+mug6Izqa+U1KF0WIiAAHwJBKWLLxS1t2yPth2w/JCkQrW2CtDvkDgTnYXQTcQdmiQ0gQAozTChCXfTeGSo");
            this.ICBB = new BarcodeWriter();
        }

        private bool generarCBB(string Codigo, string Ruta)
        {
            bool bRegresa = false; 
            try
            {
                //UnitOfMeasure measure = UnitOfMeasure.Centimeters;
                //this.ICBB.Settings.Barcode = BarcodeType.QR;
                //this.ICBB.Settings.Value = Codigo;
                //this.ICBB.Settings.Resolution = 200f;
                //this.ICBB.Settings.SetWidth(this.bcLength, measure);
                //this.ICBB.GetBarcodeAsBitmap().Save(Ruta, ImageFormat.Jpeg);

                QRCodeEncoder pEncoder = new QRCodeEncoder();
                pEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                pEncoder.QRCodeScale = 3;
                pEncoder.QRCodeVersion = 12;
                pEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

                ////string sDataCode = @"https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=DDEA4CDB-ACFF-450D-A6B4-814BFA3F7734&re=TOP1203236G5&rr=LOED840623LY3&tt=1705.01&fe=ahc5Xw==";
                ////string sRuta = @"C:\CBB.png";

                pEncoder.Encode(Codigo).Save(Ruta, System.Drawing.Imaging.ImageFormat.Png);

                bRegresa = true;
            }
            catch (Exception exception)
            {
                ////General.msjError("Error : " + exception);
                bRegresa = false;
            }

            return bRegresa;
        }

        public bool getImgCBB(string RFCEmisor, string RFCReceptor, string Total, string UUID, string Ruta)
        {
            return getImgCBB("", RFCEmisor, RFCReceptor, Total, UUID, Ruta, "", eVersionCFDI.Version__3_2);  
        }

        public bool getImgCBB(string Url_Validacion, string RFCEmisor, string RFCReceptor, string Total, string UUID, string Ruta, string Sello, eVersionCFDI tpVersionCFDI)
        {
            bool bRegresa = false;
            string str = "";
            try
            {
                if (this.Validate(RFCEmisor, RFCReceptor, Total, UUID))
                {
                    if (tpVersionCFDI == eVersionCFDI.Version__3_2)
                    {
                        str = str = "?re=" + RFCEmisor;
                        str = ((str + "&rr=" + RFCReceptor) + "&tt=" + Total) + "&id=" + UUID;
                        bRegresa = this.generarCBB(str, Ruta);
                    }

                    if (tpVersionCFDI == eVersionCFDI.Version__3_3 || tpVersionCFDI == eVersionCFDI.Version__4_0) 
                    {
                        str = ""; 
                        str += Url_Validacion+ "?id=" + UUID;
                        str += str = "&re=" + RFCEmisor;
                        str += str = "&rr=" + RFCReceptor;
                        str += "&tt=" + Total;
                        str += str = "&fe=" + Sello;
                        bRegresa = this.generarCBB(str, Ruta);
                    }
                }
            }
            catch (Exception exception)
            {
                //General.msjError("Error : " + exception);                
            }

            return bRegresa;
        }

        private bool Validate(string RFCEmisor, string RFCReceptor, string Total, string UUID)
        {
            string str = RFCEmisor.Trim();
            string str2 = RFCReceptor.Trim();
            bool bRegresa = true;  

            if ((str.Length < 12) || (str.Length > 13))
            {
                General.msjAviso("RFC Emisor inconrrecto debe contener de 12 a 13 caracteres verifique por favor", "CBB");
                bRegresa = false;
            }

            if (bRegresa)
            {
                if ((str2.Length < 12) || (str2.Length > 13))
                {
                    General.msjAviso("RFC Receptor inconrrecto debe contener de 12 a 13 caracteres verifique por favor", "CBB");
                    bRegresa = false;
                }
            }

            if (bRegresa)
            {
                if (Total.Trim() == "")
                {
                    MessageBox.Show("Cantidad incorrecta", "CBB");
                    bRegresa = false;
                }
            }

            if (bRegresa)
            {
                if (UUID.Trim() == "")
                {
                    General.msjAviso("Identificador incorrecto", "CBB");
                    bRegresa = false;
                }
            }

            return bRegresa;
        }
    }
}
